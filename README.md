# Distributed transactions in WCF

Reproduces problem with timed out SQL connections when using DTC. Examples with both EntityFramework and NHibernate.

## Install

* Open solution as administrator with elevated permissions, required since we're hosting the WCF services in IIS.
* Run "Update-Database" from the Package Manager Console, Target the Common project because it contains the migration schema.
* Set ClientProgram as Startup and Press F5 to run.

## Reproduce with NHibernate

Press numpad 1 (causes timeout)
Press numpad 1 again (causes a second timeout)
Press numpad 1 again (now we get an exception at session.BeginTransaction())

From this point on all clients that call session.BeginTransaction() will fail, meaning even clients that only want to read data (numpad2).

