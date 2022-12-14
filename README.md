# Net 6 Core Api - Boilerplate with Autofac DI

## **BREAKING** - If you are using this setup in `.Net 7 `

In `Startup.cs` You have to include following line 
```            
            // I have to include this, because it messes up automatically resolving my post/put methods
            // it was introduced as breaking change in https://learn.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/7.0/api-controller-action-parameters-di
            // Without this I would have to mark every Api Endpoint that is POST/PUT with [FromBody] attribute
            // This is directly tied to my question on GitHub: https://github.com/dotnet/aspnetcore/issues/45124
            // And this issue with autodiscovery of AutoFac https://github.com/dotnet/aspnetcore/issues/43363
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.DisableImplicitFromServicesParameters = true;
            });
```


## What ?

A `.Net 6 API` project (I won't upgrade to `.Net 7` cause my title is too specific), which includes:

- `Microsoft.AspNetCore.Identity` that is managing users and is using `JSON Web Token (JWT)` for auth. It is based on `Identity Server`, and you can easily change it to use `Identity Server` instead (see `AuthenticationHelper.cs line 57`).
- We'll use `Autofac to register our Dependency Injection` code. **I prefer to use the built in Microsoft one**, but I did it with `Autofac` just for exercise.
- I implemented a `Unit-Of-Work pattern` that meets my needs and the needs of the projects I am working on quite well. Keep in mind that this version was written to support the running of stored procedures as well as to run queries in transactions.
- Added usage of `Sequences as primary key` for table. (See the 2nd point in "Why?" below. )
- Modular `CORS` registration
- `NLog` library to do the logging (mainly exceptions for now) and saves them in the `.txt` files per day. 


## Why ?

For my and possibly your prototyping. Also, there is an ease of mind when there is a take home assignment to be done, so I can get quickly to the core of the problem without rushing to setup everything and wasting precious time.

The second reason is that I recently had an issue with supporting an old system that has a database with 500 tables that don't have Identity set as PK. I had to use `SQL Sequences` to achieve this (https://github.com/dotnet/efcore/issues/26480), and I think there is really a huge gap with supporting things that do not fit the "newest and greatest" narrative.
**Sometimes you just have to support legacy, and that's it.**

## Before running the project

Nothing. Optionally, you can create a database called `BloggingDb` on your preferred database server (in my case, `localhost`).

## Run the project

```
1. Open npm console (Visual studio: View -> Other windows -> Package Manager Console)
2. Type command: Update-Database to EF Core run migrations
3. Run the api project
```

# Note for 401

If you get a `401 Status Code`, it means you are not authorized. You can either remove the `[Authorize]` attribute on the `Controller` or login and add the bearer token to the top of the page with `Swagger`.


# Plans for future? 

I think it would be good to add some example code for the `EF Interceptors` and `EF Global filters`. 
