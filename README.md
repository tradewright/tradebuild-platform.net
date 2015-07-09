# tradebuild-platform.net
TradeBuild Trading Platform.Net is a set of .Net components for creating automated and manual financial trading systems.

It is being developed by porting from the Visual Basic 6 version, which can be found at https://github.com/rlktradewright/tradebuild-platform.

The porting strategy is as follows:

* I'll attempt to keep a working system at all times. Thus the intention is that there will always be a working version of various demo programs, such as the TradeSkil Demo manual trading client.

* The initial version will use the VB6 components to support the demo programs, which themselves will be ported to .Net versions.

* In general components will be ported from the bottom of the dependency tree upwards. The first pass through this process will basically be a straight port, without taking advantage of advanced .Net language capabilities. Thus factory methods may be replaced by contructors, and type inference used where appropriate, but at this stage there will be no attempt to use techniques such as Async.

* When a component has been ported, any component higher in the dependency tree (and in particular the demo applications) that has already been ported will be changed to use the ported component.

* Each public class of a ported component will implement the default and event interfaces of the corresponding coClass from the VB6 implementation. This means that higher levels of the dependency tree that have not yet been ported will continue to work with objects created in lower levels that have been ported.

* Once this first pass is complete, improvements will be made to take full advantage of .Net.

Note that the VB6 version makes heavy use of the TradeWright Utilities components at https://github.com/rlktradewright/tradewright-common, and these are also being ported to .Net in a parallel project (see https://github.com/tradewright/tradewright-common.net).



