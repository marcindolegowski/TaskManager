# TaskManager
This is simple task manager application. 

# How to run project

Just open project in Visual Studio 2019, and configure multiple startup projects. Following projects should be start:

 - TaskManager
 - TaskManagerValidationService
 - TaskManagerWebSite
 
 Now you can run application, then in one window you should be able to see application interface.

## TaskManager
TaskManager is REST service which was created in **WebApi** with **.NET Core 3.0**. I am using Swagger libary to create documentation. 

## TaskManagerValidationService
TaskManagerValidationService  is REST service which was created in **WebApi** with **.NET Core 3.0**. This service contains only one `GET` method `IsTaskNameUnique(string taskName)`.
I am using InMemoryDb to store task names. At the start of application two rows are added to table `Tasks`, which stores task names. Names of these tasks:

- Task 1
- Task 2

## TaskManagerWebSite
This is static website hosted on IIS server. Interface was creted in **React** and **Redux**.

## TaskManagerTests
Unit tests. I used **NUnit** framework and **Moq** library.

If you have any questions or need further information, please feel free to contact me. 
My email address: **m.dolegowski92@gmail.com**
