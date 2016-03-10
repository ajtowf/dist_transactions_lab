# Distributed transactions in WCF with async/await

Solution to test how distributed transactions behave in WCF with async await keywords, see [blog post here](http://www.towfeek.se/2016/03/connection-leaks-when-using-asyncawait-with-transactions-in-wcf/)

## Install

* Open solution as administrator with elevated permissions, required since we're hosting the WCF services in IIS.
* Run "Update-Database" from the Package Manager Console
* Press F5 to run

## NHibernate or EntityFramework

You can easily switch between these OR-mappers by changing witch implementation of IDbAbstraction to use in the service implementation, [see my screencast](https://www.youtube.com/watch?v=PHLyJSOQmJA) to see how this is done.
