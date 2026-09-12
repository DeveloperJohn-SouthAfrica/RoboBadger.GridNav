# Robo Badger GridNav 

## Installation

### Install Git on Windows using winget

```shell    
winget install git
```

### Install Git on Linux using apt
```
sudo apt install git
```

### Install the dotnet sdk and hosting bundle on Linux using apt
```
sudo apt install dotnet-sdk-10
sudo apt install aspnetcore-runtime-10
```

## Download the source code

```shell
git clone https://github.com/DeveloperJohn-SouthAfrica/RoboBadger.GridNav.git
```

### Run tests to ensure a stable install

```shell
cd RoboBadger.GridNav/RoboBadger.GridNav.Core.Tests/
# If you are used to running dotnet test, this has changes in net 10
dotnet run
```

### Run the TUI for a brief walk through the GridNav system

```shell
cd RoboBadger.GridNav/RoboBadger.GridNav.Core/
dotnet run
```
---

## License

This software is licensed under THE HIRE-ME-WARE LICENSE
you can read more about it here [LICENSE](LICENSE)

## My approach

1. Test first.
2. Solve the hard part first (interpreter).
3. Add behavior (crawl/lost/bounce).
4. Refactor to clean SOLID services.
5. Validate constraints and ship with a TUI.

## What the roadmap might look like from here
Tl;DR: 

`Minimal API -> Domain model -> Infrastructure -> Data storage -> Atomic pub/sub for work units -> UI.`

1. Start with a minimal ASP.NET Core API to expose clean endpoints.
2. Model the domain so business rules live in the core, not the UI.
3. Add an infrastructure layer for adapters, IO, and integrations.
4. Introduce persistent storage with a simple, reliable data stack.
5. Add atomic pub/sub for unit-of-work processing and event flow.
6. Finish with a UI for operator workflows and visibility.

## Stack Choice

### Tech stack choice

I chose .NET because it is compiled voiding runtime errors at compile time, instead of interpreted languages. Its cross-platform, mature, and actively maintained.  
It supports strong secure-by-default patterns and a robust tooling ecosystem, which helps reduce risk in production delivery.  
Its testing ecosystem (TDD/BDD workflows) and JetBrains tooling support fast concept to code scenarios.  
This combination of reliability, maintainability, and development speed is a major reason it is widely used in enterprise environments, including banking and finance.

## Documentation

```shell
dotnet tool install --global dotnet-document
```










