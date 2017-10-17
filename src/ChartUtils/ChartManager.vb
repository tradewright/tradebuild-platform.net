#Region "License"

' The MIT License (MIT)
'
' Copyright (c) 2017 Richard L King (TradeWright Software Systems)
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

#End Region

Imports ChartSkil27
Imports StudyUtils27
Imports System.Collections.Generic
Imports System.Runtime.InteropServices.Marshal
Imports BarUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Charts.BarFormatters

Public NotInheritable Class ChartManager
    Implements ITaskCompletionListener

#Region "Interfaces"


#End Region

#Region "Events"

    Event BaseStudyConfigurationChanged(sender As Object, studyConfig As StudyConfiguration)
    Event StudyAdded(sender As Object, study As _IStudy)
    Event StudyRemoved(sender As Object, study As _IStudy)

#End Region

#Region "Constants"

    Private Const ModuleName As String = "ChartManager"

    Private Const ConfigSectionBaseStudyConfig As String = "BaseStudyConfig"

#End Region

#Region "Enums"

#End Region

#Region "Types"

    Private Structure RegionEntry
        Dim region As ChartRegion
        Dim usageCount As Integer
    End Structure

#End Region

#Region "Member variables"

    Private mChart As AxChartSkil27.AxChart
    Private mStudyLibManager As StudyLibraryManager
    Private mStudyManager As StudyManager
    Private mBarFormatterLibManager As BarFormatterLibManager
    Private mIncludeBarsOutsideSession As Boolean

    Private mRegionEntries As IDictionary(Of String, RegionEntry) = New Dictionary(Of String, RegionEntry)

    ' we use this collection to locate studyConfigurations from study objects
    Private mAllStudyConfigs As IDictionary(Of String, StudyConfiguration) = New Dictionary(Of String, StudyConfiguration)

    Private mInstanceFullyQualifiedNames As New Collection

    Private mUpdatePerTick As Boolean = True

    Private mPeriods As Periods

    ' the study on which all others are built
    Private mBaseStudy As _IStudy
    Private mBaseStudyConfig As StudyConfiguration
    Private mBars As Bars

    Private mConfig As ConfigurationSection

    Private WithEvents mFutureWaiter As New FutureWaiter

#End Region

#Region "Constructors"

    Public Sub New(studyManager As StudyManager,
                   chart As AxChartSkil27.AxChart,
                   barFormatterLibManager As BarFormatterLibManager,
                   includeBarsOutsideSession As Boolean)
        MyBase.New()

        mStudyManager = studyManager
        mStudyLibManager = mStudyManager.StudyLibraryManager
        mChart = chart
        mBarFormatterLibManager = barFormatterLibManager
        mIncludeBarsOutsideSession = includeBarsOutsideSession
        mPeriods = mChart.Periods
    End Sub

#End Region

#Region "ITaskCompletionListener Interface Members"

    Private Sub TaskCompletionListener_taskCompleted(ByRef ev As TaskCompletionEventData) Implements ITaskCompletionListener.TaskCompleted
        If mChart IsNot Nothing Then mChart.EnableDrawing()
    End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

    Private Sub mFutureWaiter_WaitCompleted(ev As FutureWaitCompletedEventData)
        If ev.Future.IsAvailable Then mBars = DirectCast(ev.Future.Value, Bars)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property BarFormatterLibManager() As BarFormatterLibManager
        Get
            Return mBarFormatterLibManager
        End Get
    End Property

    Public ReadOnly Property BaseStudy() As _IStudy
        Get
            Return mBaseStudy
        End Get
    End Property

    Public ReadOnly Property BaseStudyConfiguration() As StudyConfiguration
        Get
            If mBaseStudyConfig IsNot Nothing Then
                Return mBaseStudyConfig.Clone ' return a defensive copy
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property Chart() As AxChartSkil27.AxChart
        Get
            Return mChart
        End Get
    End Property

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            If Not mBaseStudyConfig Is Nothing Then
                mBaseStudyConfig.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionBaseStudyConfig)
            End If
        End Set
    End Property

    Public ReadOnly Property RegionNames() As String()
        Get
            If mRegionEntries.Count() = 0 Then Return Nothing

            Dim names(mRegionEntries.Count() - 1) As String

            Dim i = 0
            For Each re In mRegionEntries.Values
                names(i) = re.region.Name
                i += 1
            Next
            Return names
        End Get
    End Property

    Public ReadOnly Property StudyLibraryManager() As StudyLibraryManager
        Get
            Return mStudyLibManager
        End Get
    End Property

    Public WriteOnly Property UpdatePerTick() As Boolean
        Set
            mUpdatePerTick = Value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Function AddStudyConfiguration(
                    studyConfig As StudyConfiguration,
                    Optional numberOfValuesToReplay As Integer = ReplayNumbers.ReplayAll) As _IStudy
        Dim lStudy = addStudy(studyConfig)

        removeExistingStudyConfig(lStudy)

        ApplyStudyConfiguration(studyConfig, numberOfValuesToReplay)

        Return lStudy
    End Function

    Public Sub ApplyStudyConfiguration(
                ByVal studyConfig As StudyConfiguration,
                Optional ByVal numberOfValuesToReplay As Integer = ReplayNumbers.ReplayAll)
        mChart.DisableDrawing()
        doApplyStudyConfiguration(studyConfig, numberOfValuesToReplay)
        addStudyConfigToParent(studyConfig)
        addStudyConfig(studyConfig)
        mChart.EnableDrawing()
        RaiseEvent StudyAdded(Me, studyConfig.Study)
    End Sub

    Public Sub ClearChart()
        mChart.ClearChart()

        'ReleaseComObject(mPeriods)
        mPeriods = mChart.Periods

        If Not mBaseStudyConfig Is Nothing Then
            mBaseStudyConfig.Finish()
            mBaseStudyConfig.RemoveFromConfig()
            mBaseStudyConfig = Nothing

            'ReleaseComObject(mBaseStudy)
            mBaseStudy = Nothing
        End If

        mAllStudyConfigs.Clear()

        For Each re As RegionEntry In mRegionEntries.Values
            'ReleaseComObject(re.region)
        Next
        mRegionEntries.Clear()

        If mBars IsNot Nothing Then
            'ReleaseComObject(mBars)
            mBars = Nothing
        End If
    End Sub

    Public Sub Finish()
        'ReleaseComObject(mStudyManager)
        mStudyManager = Nothing

        mAllStudyConfigs = Nothing

        'ReleaseComObject(mPeriods)
        mPeriods = Nothing

        If Not mBaseStudyConfig Is Nothing Then
            mBaseStudyConfig.Finish()
            mBaseStudyConfig = Nothing

            'ReleaseComObject(mBaseStudy)
            mBaseStudy = Nothing
        End If
        mRegionEntries = Nothing

        If mBars IsNot Nothing Then
            'ReleaseComObject(mBars)
            mBars = Nothing
        End If
    End Sub

    Public Function GetDefaultStudyConfiguration(
                ByVal Name As String,
                ByVal studyLibName As String) As StudyConfiguration
        Return ChartUtils.GetDefaultStudyConfiguration(Name, mStudyLibManager, studyLibName)
    End Function

    Friend Function GetPeriod(timestamp As Date) As Period
        Static sPeriod As Period
        Static sTimestamp As Date

        If timestamp = sTimestamp Then
            If Not sPeriod Is Nothing Then
                Return sPeriod
                Exit Function
            End If
        End If

        Try
            sPeriod = mPeriods.Item(timestamp)
        Catch
            sPeriod = mPeriods.Add(timestamp)
        End Try
        sTimestamp = timestamp
        Return sPeriod
    End Function

    Friend Function GetSpecialValue(valueType As StudyValueHandler.SpecialValues) As Object
        Select Case valueType
            Case StudyValueHandler.SpecialValues.SVCurrentSessionEndTime
                Return mChart.CurrentSessionEndTime
            Case StudyValueHandler.SpecialValues.SVCurrentSessionStartTime
                Return mChart.CurrentSessionStartTime
            Case StudyValueHandler.SpecialValues.SVHighPrice
                Return mBars.HighValue
            Case StudyValueHandler.SpecialValues.SVLowPrice
                Return mBars.LowValue
            Case StudyValueHandler.SpecialValues.SVPreviousClosePrice
                Return mBars.CloseValue(-1)
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function GetStudyConfiguration(
                studyId As String) As StudyConfiguration
        Return mAllStudyConfigs(studyId)
    End Function

    Friend Function GetXFromTimestamp(
                ByVal timestamp As Date) As Double
        Return mChart.GetXFromTimestamp(timestamp)
    End Function

    Public Sub LoadFromConfig(
                config As ConfigurationSection,
                baseStudy As _IStudy,
                includeBarsOutsideSession As Boolean)
        mConfig = config
        mIncludeBarsOutsideSession = includeBarsOutsideSession

        If Not mConfig Is Nothing Then
            Dim baseStudySect = mConfig.GetConfigurationSection(ConfigSectionBaseStudyConfig)
            Dim studyConfig = New StudyConfiguration
            studyConfig.Study = baseStudy
            studyConfig.UnderlyingStudy = mStudyManager.GetUnderlyingStudy(DirectCast(baseStudy, _IStudy))

            studyConfig.LoadFromConfig(baseStudySect)

            setTheBaseStudyConfiguration(studyConfig, ReplayNumbers.ReplayAll)
        End If
    End Sub

    Public Sub NotifyInput(inputHandle As Integer, inputValue As Object, timestamp As Date)
        mStudyManager.NotifyInput(inputHandle, inputValue, timestamp)
    End Sub

    Public Sub RemoveStudyConfiguration(studyConfig As StudyConfiguration)
        mChart.DisableDrawing()
        doUnApplyStudyConfiguration(studyConfig)

        Dim sc As StudyConfiguration
        For Each sc In studyConfig.StudyConfigurations
            RemoveStudyConfiguration(sc)
        Next

        removeStudyConfigFromParent(studyConfig)
        removeStudyConfig(studyConfig)

        mChart.EnableDrawing()
        RaiseEvent StudyRemoved(Me, studyConfig.Study)
    End Sub

    Public Function ReplaceStudyConfiguration(
                    oldStudyConfig As StudyConfiguration,
                    newStudyConfig As StudyConfiguration,
                    Optional numberOfValuesToReplay As Integer = ReplayNumbers.ReplayAll) As _IStudy
        If oldStudyConfig.Study Is BaseStudy Then
            newStudyConfig.Study = oldStudyConfig.Study
            SetBaseStudyConfiguration(newStudyConfig)
            ReplaceStudyConfiguration = newStudyConfig.Study
        Else
            mChart.DisableDrawing()

            ReplaceStudyConfiguration = addStudy(newStudyConfig)
            doApplyStudyConfiguration(newStudyConfig, numberOfValuesToReplay)

            doUnApplyStudyConfiguration(oldStudyConfig)

            removeStudyConfigFromParent(oldStudyConfig)
            removeStudyConfig(oldStudyConfig)

            addStudyConfigToParent(newStudyConfig)
            addStudyConfig(newStudyConfig)

            If newStudyConfig.Study Is oldStudyConfig.Study Then
                moveChildStudyConfigs(oldStudyConfig, newStudyConfig)
            Else
                RaiseEvent StudyAdded(Me, newStudyConfig.Study)
                reconfigureChildStudies(oldStudyConfig, newStudyConfig, numberOfValuesToReplay)
                RaiseEvent StudyRemoved(Me, oldStudyConfig.Study)
                StartStudy(newStudyConfig.Study)
            End If

            oldStudyConfig.RemoveFromConfig()
            oldStudyConfig.Finish()

            mChart.EnableDrawing()
        End If
    End Function

    Public Sub ScrollToTime(time As Date)
        Dim periodNumber = mPeriods.Item(time).PeriodNumber
        mChart.LastVisiblePeriod = periodNumber + CInt((mChart.LastVisiblePeriod - mChart.FirstVisiblePeriod) / 2) - 1
    End Sub

    Public Sub SetBaseStudyConfiguration(value As StudyConfiguration, Optional numberOfValuesToReplay As Integer = ReplayNumbers.ReplayAll)
        If Not TypeOf value.Study Is IBarStudy Then Throw New ArgumentException("Base study must implement the BarStudy interface")

        Dim sc = value.Clone ' take a defensive copy
        sc.ClearStudyValueHandlers()
        sc.ClearStudyConfigs()
        setTheBaseStudyConfiguration(sc, numberOfValuesToReplay)
    End Sub

    Public Sub SetInputRegion(inputHandle As Integer, chartRegionName As String)
        Dim inputDescr = mStudyManager.GetInputDescriptor(inputHandle)

        Dim lInputHandler = DirectCast(inputDescr.StudyInputHandler, _IStudy)

        Dim studyConfig As StudyConfiguration
        If mAllStudyConfigs.ContainsKey(lInputHandler.Id) Then
            studyConfig = mAllStudyConfigs.Item(lInputHandler.Id)
        Else
            studyConfig = New StudyConfiguration
            studyConfig.Study = lInputHandler
            studyConfig.Name = lInputHandler.InstanceName
            mAllStudyConfigs.Add(lInputHandler.Id, studyConfig)
        End If

        ' need to do this here to ensure we have the definition with all inputs
        'studyConfig.studyDefinition = inputDescr.lInputHandler.studyDefinition

        studyConfig.StudyValueConfigurations.Add(inputDescr.InputName).ChartRegionName = chartRegionName
    End Sub

    Public Function StartStudy(pStudy As _IStudy) As TaskController
        Dim lTc = mStudyManager.StartStudy(pStudy, ReplayNumbers.ReplayAll)

        If lTc Is Nothing Then Return Nothing

        lTc.AddTaskCompletionListener(Me)
        mChart.DisableDrawing()
        Return lTc
    End Function

    Public Sub UnApplyStudyConfiguration(
                ByVal studyConfig As StudyConfiguration)
        doUnApplyStudyConfiguration(studyConfig)
        removeStudyConfigFromParent(studyConfig)
        removeStudyConfig(studyConfig)
        RaiseEvent StudyRemoved(Me, studyConfig.Study)
    End Sub

    Public Sub UpdateLastBar()
        If Not mBaseStudyConfig Is Nothing Then mBaseStudyConfig.Update()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub addDependentStudies(studyConfig As StudyConfiguration)
        For Each sc As StudyConfiguration In studyConfig.StudyConfigurations
            sc.UnderlyingStudy = studyConfig.Study
            AddStudyConfiguration(sc)
            StartStudy(sc.Study)
            addDependentStudies(sc)
        Next
    End Sub

    Private Function addStudy(
                    studyConfig As StudyConfiguration) As _IStudy
        If mBaseStudy Is Nothing Then Throw New InvalidOperationException("Base Study Configuration has not yet been set")
        If studyConfig Is Nothing Then Throw New NullReferenceException()

        If studyConfig.UnderlyingStudy Is Nothing Then
            studyConfig.UnderlyingStudy = mBaseStudy
        End If

        Dim lStudy = DirectCast(mStudyManager.AddStudy(studyConfig.Name,
                                    studyConfig.UnderlyingStudy,
                                    studyConfig.InputValueNames,
                                    mIncludeBarsOutsideSession,
                                    studyConfig.Parameters,
                                    studyConfig.StudyLibraryName), _IStudy)
        If lStudy Is mBaseStudy Then Throw New InvalidOperationException("Use the BaseStudyConfiguration property to set the base Study config")

        studyConfig.Study = lStudy
        Return lStudy
    End Function

    Private Sub addStudyConfig(
                    studyConfig As StudyConfiguration)
        If studyConfig.InstanceFullyQualifiedName = "" Then
            studyConfig.InstanceFullyQualifiedName = generateInstanceFullyQualifiedName(studyConfig.Study)
        End If

        mAllStudyConfigs.Add(studyConfig.Study.Id, studyConfig)
        mInstanceFullyQualifiedNames.Add(studyConfig.InstanceFullyQualifiedName, studyConfig.InstanceFullyQualifiedName)
    End Sub

    Private Sub addStudyConfigToParent(
                ByVal studyConfig As StudyConfiguration)
        If studyConfig.Study Is mBaseStudy Then Exit Sub

        Dim parentStudyConfig As StudyConfiguration
        parentStudyConfig = getParentStudyConfig(studyConfig)
        parentStudyConfig.StudyConfigurations.Add(studyConfig)
    End Sub

    Private Sub adjustRegionMinimumHeight(region As ChartRegion, yScaleQuantum As Double)
        If yScaleQuantum <> 0 Then
            If yScaleQuantum = 1 Then region.IntegerYScale = True
            If region.YScaleQuantum = 0 Or region.YScaleQuantum < yScaleQuantum Then region.YScaleQuantum = yScaleQuantum
            'If region.MinimumHeight = 0 Or region.MinimumHeight < yScaleQuantum Then region.MinimumHeight = yScaleQuantum
        End If
    End Sub

    Private Sub determineDefaultRegionName(studyConfig As StudyConfiguration)
        If studyConfig.ChartRegionName = ChartUtils.ChartRegionNameUnderlying Or
            studyConfig.ChartRegionName = ChartUtils.ChartRegionNameDefault Or
            studyConfig.ChartRegionName = "" _
        Then
            studyConfig.ChartRegionName = getUnderlyingStudyRegionName(studyConfig)
        ElseIf studyConfig.ChartRegionName = ChartUtils.ChartRegionNameCustom Then
            studyConfig.ChartRegionName = studyConfig.Study.InstancePath
        End If
    End Sub

    Private Sub doApplyStudyConfiguration(
                ByVal studyConfig As StudyConfiguration,
                ByVal numberOfValuesToReplay As Integer)
        setupStudyValueListeners(studyConfig, numberOfValuesToReplay)
        setupHorizontalRules(studyConfig)
    End Sub

    Private Sub doUnApplyStudyConfiguration(
                ByVal studyConfig As StudyConfiguration)
        mChart.DisableDrawing()
        removeStudyValueListeners(studyConfig)
        removeHorizontalRules(studyConfig)
        mChart.EnableDrawing()
    End Sub

    Private Function getParentStudyConfig(ByVal pStudyConfig As StudyConfiguration) As StudyConfiguration
        If mAllStudyConfigs.ContainsKey(pStudyConfig.UnderlyingStudy.Id) Then
            Return mAllStudyConfigs(pStudyConfig.UnderlyingStudy.Id)
        End If
        Return Nothing
    End Function

    Private Function getRegion(regionName As String, title As String, incrementUsageCount As Boolean, yScaleQuantum As Double) As ChartRegion
        Dim re As RegionEntry = Nothing

        If mRegionEntries.TryGetValue(regionName, re) Then
            getRegion = re.region
            If incrementUsageCount Then
                re.usageCount = re.usageCount + 1
                mRegionEntries.Remove(regionName)
                mRegionEntries.Add(regionName, re)
            End If
        Else
            If mChart.Regions.Contains(regionName) Then
                getRegion = mChart.Regions.Item(regionName)
                adjustRegionMinimumHeight(getRegion, yScaleQuantum)
            Else
                getRegion = mChart.Regions.Add(20, , , , regionName)
                getRegion.YGridlineSpacing = 0.8
                adjustRegionMinimumHeight(getRegion, yScaleQuantum)
                getRegion.Title.Text = title
                getRegion.Title.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
            End If
            re.region = getRegion
            If incrementUsageCount Then re.usageCount = 1
            mRegionEntries.Add(regionName, re)
        End If
    End Function

    Private Function getRegionName(studyConfig As StudyConfiguration, studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition, study As _IStudy) As String
        Dim regionName As String
        If studyValueConfig.ChartRegionName = ChartUtils.ChartRegionNameDefault Or studyValueConfig.ChartRegionName = "" Then
            ' then use the study's default region
            regionName = studyConfig.ChartRegionName
        ElseIf studyValueConfig.ChartRegionName = ChartUtils.ChartRegionNameCustom Then
            regionName = studyValueDef.Name & "." & study.InstancePath
        ElseIf studyValueConfig.ChartRegionName = ChartUtils.ChartRegionNameUnderlying Then
            regionName = getUnderlyingStudyRegionName(studyConfig)
        Else
            regionName = studyValueConfig.ChartRegionName
        End If
        Return regionName
    End Function

    Private Function getUnderlyingStudyRegionName(pStudyConfig As StudyConfiguration) As String
        Return getParentStudyConfig(pStudyConfig).ChartRegionName
    End Function

    Private Sub includeStudyValueInChart(
                    studyConfig As StudyConfiguration,
                    studyValueConfig As StudyValueConfiguration,
                    studyValueDef As StudyValueDefinition,
                    numberOfValuesToReplay As Integer)
        Dim study = studyConfig.Study
        Dim regionName = getRegionName(studyConfig, studyValueConfig, studyValueDef, study)
        Dim region = getRegion(regionName, study.InstanceName, True, study.GetValueTicksize(studyValueConfig.ValueName))

        Dim lStudyValueHandler = studyConfig.StudyValueHandlers.Add(Me, region, study, studyValueConfig, studyValueDef, mUpdatePerTick)

        Dim lTaskController = study.AddStudyValueListener(
                                                    lStudyValueHandler,
                                                    studyValueConfig.ValueName,
                                                    numberOfValuesToReplay,
                                                    "AddStudyValueListener for value " & studyValueConfig.ValueName & " to study " & study.InstanceName)

        If Not lTaskController Is Nothing Then
            lTaskController.AddTaskCompletionListener(Me)
            mChart.DisableDrawing()
        End If
    End Sub

    Private Function isRegionInUse(regionName As String) As Boolean
        Dim re As RegionEntry = Nothing
        If mRegionEntries.TryGetValue(regionName, re) Then
            Return re.region IsNot Nothing
        Else
            Return False
        End If
    End Function

    Private Sub moveChildStudyConfigs(oldStudyConfig As StudyConfiguration, newStudyConfig As StudyConfiguration)
        For Each childStudyConfig As StudyConfiguration In oldStudyConfig.StudyConfigurations
            newStudyConfig.StudyConfigurations.Add(childStudyConfig)
        Next
        oldStudyConfig.StudyConfigurations.Clear()
    End Sub

    Private Sub reconfigureChildStudies(
                    oldStudyConfig As StudyConfiguration,
                    newStudyConfig As StudyConfiguration,
                    numberOfValuesToReplay As Integer)
        For Each oldChildStudyConfig As StudyConfiguration In oldStudyConfig.StudyConfigurations
            Dim newChildStudyConfig = oldChildStudyConfig.Clone
            newChildStudyConfig.StudyValueHandlers = New StudyValueHandlers
            newChildStudyConfig.UnderlyingStudy = newStudyConfig.Study
            If oldChildStudyConfig.ChartRegionName = oldStudyConfig.ChartRegionName Then
                newChildStudyConfig.ChartRegionName = newStudyConfig.ChartRegionName
            End If

            doApplyStudyConfiguration(newChildStudyConfig, numberOfValuesToReplay)
            doUnApplyStudyConfiguration(oldChildStudyConfig)

            reconfigureChildStudies(oldChildStudyConfig, newChildStudyConfig, numberOfValuesToReplay)

            removeStudyConfig(oldChildStudyConfig)

            addStudyConfigToParent(newChildStudyConfig)
            addStudyConfig(newChildStudyConfig)

            StartStudy(newChildStudyConfig.Study)
        Next
    End Sub

    Private Sub removeExistingStudyConfig(pStudy As _IStudy)
        If mAllStudyConfigs.Keys.Contains(pStudy.Id) Then
            RemoveStudyConfiguration(mAllStudyConfigs(pStudy.Id))
        End If

    End Sub

    Private Sub removeStudyConfig(studyConfig As StudyConfiguration)
        If studyConfig.Study Is mBaseStudy Then
            mBaseStudyConfig.RemoveFromConfig()
        Else
            Dim parentStudyConfig = mAllStudyConfigs(studyConfig.UnderlyingStudy.Id)

            parentStudyConfig.StudyConfigurations.Remove(studyConfig)
        End If

        mAllStudyConfigs.Remove(studyConfig.Study.Id)
        mInstanceFullyQualifiedNames.Remove(studyConfig.InstanceFullyQualifiedName)
    End Sub

    Private Sub removeStudyConfigFromParent(ByVal pStudyConfig As StudyConfiguration)
        Dim parentStudyConfig As StudyConfiguration
        If pStudyConfig.Study Is mBaseStudy Then Exit Sub

        parentStudyConfig = getParentStudyConfig(pStudyConfig)
        parentStudyConfig.StudyConfigurations.Remove(pStudyConfig)
    End Sub

    Private Sub removeHorizontalRules(ByVal studyConfig As StudyConfiguration)
        Dim horizRulesLineSeries = studyConfig.HorizontalRulesLineSeries
        If horizRulesLineSeries Is Nothing Then Exit Sub

        Dim re As RegionEntry = Nothing
        If mRegionEntries.TryGetValue(studyConfig.ChartRegionName, re) Then
            re.region.RemoveGraphicObjectSeries(DirectCast(horizRulesLineSeries, IGraphicObjectSeries))
        End If
    End Sub

    Private Sub removeStudyValueListeners(studyConfig As StudyConfiguration)
        For Each svh As StudyValueHandler In studyConfig.StudyValueHandlers
            studyConfig.Study.RemoveStudyValueListener(svh)
            Dim regionName = svh.Region.Name
            Dim re = mRegionEntries(regionName)
            If re.usageCount = 1 Then
                mChart.Regions.Remove(re.region)
                mRegionEntries.Remove(regionName)
            Else
                re.region.RemoveGraphicObjectSeries(svh.ValueSeries)
                mRegionEntries.Remove(regionName)
                re.usageCount = re.usageCount - 1
                mRegionEntries.Add(regionName, re)
            End If
        Next

        studyConfig.StudyValueHandlers.Clear()
    End Sub

    Private Sub setBaseStudyConfig(
                    studyConfig As StudyConfiguration)
        If Not mBaseStudyConfig Is Nothing Then
            mBaseStudyConfig.Finish()
            mBaseStudyConfig.RemoveFromConfig()
        End If

        mBaseStudyConfig = studyConfig
        mBaseStudy = studyConfig.Study

        mFutureWaiter.Add(DirectCast(mBaseStudy, IBarStudy).BarsFuture)
        If Not mConfig Is Nothing Then
            mBaseStudyConfig.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionBaseStudyConfig)
        End If
    End Sub

    Private Sub setTheBaseStudyConfiguration(studyConfig As StudyConfiguration, numberOfValuesToReplay As Integer)
        If mBaseStudy Is Nothing Then
            setBaseStudyConfig(studyConfig)

            doApplyStudyConfiguration(studyConfig, numberOfValuesToReplay)
            addStudyConfig(studyConfig)

            RaiseEvent StudyAdded(Me, studyConfig.Study)

            addDependentStudies(studyConfig)
        Else
            doApplyStudyConfiguration(studyConfig, numberOfValuesToReplay)
            doUnApplyStudyConfiguration(mBaseStudyConfig)

            If studyConfig.Study Is mBaseStudy Then
                moveChildStudyConfigs(mBaseStudyConfig, studyConfig)
            Else
                RaiseEvent StudyAdded(Me, studyConfig.Study)
                reconfigureChildStudies(mBaseStudyConfig, studyConfig, numberOfValuesToReplay)
            End If

            removeStudyConfig(mBaseStudyConfig)
            addStudyConfig(studyConfig)

            setBaseStudyConfig(studyConfig)
        End If

        RaiseEvent BaseStudyConfigurationChanged(Me, studyConfig)
    End Sub

    Private Function generateInstanceFullyQualifiedName(
                ByVal pStudy As _IStudy) As String
        Dim lKey = pStudy.InstancePath

        If mInstanceFullyQualifiedNames.Contains(lKey) Then
            Dim i As Long
            Do
                i = i + 1
            Loop Until Not mInstanceFullyQualifiedNames.Contains(lKey & "." & i)
            lKey = lKey & "." & i
        End If

        Return lKey
    End Function

    Private Sub setupHorizontalRules(ByVal studyConfig As StudyConfiguration)
        If studyConfig.StudyHorizontalRules.count = 0 Then Return
        If Not isRegionInUse(studyConfig.ChartRegionName) Then Return

        Dim Region = getRegion(studyConfig.ChartRegionName, studyConfig.Study.InstanceName, False, 0.0)

        Dim horizRulesLineSeries = DirectCast(Region.AddGraphicObjectSeries(DirectCast(New LineSeries, IGraphicObjectSeries), LayerNumbers.LayerGrid + 1), ChartSkil27.LineSeries)
        horizRulesLineSeries.Extended = True
        horizRulesLineSeries.ExtendAfter = True
        horizRulesLineSeries.ExtendBefore = True

        For Each studyHorizRule As StudyHorizontalRule In studyConfig.StudyHorizontalRules
            Dim line = horizRulesLineSeries.Add
            line.Color = studyHorizRule.color
            line.LineStyle = studyHorizRule.style
            line.Thickness = studyHorizRule.thickness
            line.Point1 = ChartUtils.ChartSkil.NewPoint(0, studyHorizRule.y, CoordinateSystems.CoordsRelative, CoordinateSystems.CoordsLogical)
            line.Point2 = ChartUtils.ChartSkil.NewPoint(100, studyHorizRule.y, CoordinateSystems.CoordsRelative, CoordinateSystems.CoordsLogical)
        Next
        studyConfig.HorizontalRulesLineSeries = horizRulesLineSeries
    End Sub

    Private Sub setupStudyValueListeners(studyConfig As StudyConfiguration, numberOfValuesToReplay As Integer)
        Dim studyValueDefs = studyConfig.Study.StudyDefinition.StudyValueDefinitions

        determineDefaultRegionName(studyConfig)

        For Each studyValueConfig As StudyValueConfiguration In studyConfig.StudyValueConfigurations
            If studyValueConfig.IncludeInChart Then
                includeStudyValueInChart(studyConfig,
                                        studyValueConfig,
                                        studyValueDefs.Item(studyValueConfig.ValueName),
                                        numberOfValuesToReplay)
            End If
        Next
    End Sub

#End Region

End Class