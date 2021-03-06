# Installation

To run this project you must have .NetCore installed. If you don't, please download the .NetCore Runtime from [here](https://dotnet.microsoft.com/download)

To build and debug you need to install the SDK

## Version
The current solution runs netcore 3.1

## Linux Installation

### SDK
```bash
wget -q https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```

```bash
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-3.1
```

### Runtime
```bash
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-runtime-3.1
```

## Mac Installation

### SDK
Link to installer: [https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-macos-x64-installer](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-macos-x64-installer)

### Runtime
Link to installer: [https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-3.1.0-macos-x64-installer](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-3.1.0-macos-x64-installer)

# Building
Once the SDK is installed, we can build and debug.

For building use the followind command:
```bash
dotnet build
```

You can set a release build setting the following flag option
```bash
dotnet build -c Release 
```

Once it's built, according to the flag configuration it will generate a bin folder inside each project directory. The desired program will be in the following directory:
**/bin/{Configuration}/netcoreapp3.1/{projectName}**

Example:
**Nubank.Authorizer/bin/Release/netcoreapp3.1/Nubank.Authorizer**

# Publish
For publishig to a platform use the following command:

For building use the followind command (ubuntu 16.04):
```bash
dotnet publish -c release -r ubuntu.16.04-x64
```

# Execution
Once it's published you can run the program

```bash
./Nubank.Authorizer < operations
```

Assuming operations have the correct format:
```
cat operations
{ "account": { "activeCard": true, "availableLimit": 100 } }
{ "transaction": { "merchant": "Burger King", "amount": 20, "time": "2019-02-13T10:00:00.000Z" } }
{ "transaction": { "merchant": "Habbib's", "amount": 90, "time": "2019-02-13T11:00:00.000Z" } }
```

# Solution Design
The solution has many layers with different objectives:
- Nubank.Authorizer: I/O Interaction Layer
- Nubank.Contract: Contract Layer
- Nubank.Domain: Business Logic Layer
- Nubank.Persistence: Persistence Layer
- Nubank.Tests: Unit Tests + Integration Test
- Nubank.Tools: Set of tools, classes, helpers, errors that can be used in all layers

## Authorizer
This projects handles the input and output of the program, the serialization and deserialization of json and implements the dependency injection pattern on the Program.cs

To gain all of the net core di power the program raises a host which handles the injection and execution.

## Domain
The domain has 2 different core structures:
- Operation 
- BusinessValidations

The program understands each json row as an operation.
Each operation executes a process. 
To execute a process, first it provides a way to build itself and then executes some validations. 
Once all validations have passed, then the operation executes. 

This layer core logic is located in the Operation class.

To extend Validations implement the BusinessValidation abstract class and add the new Validation to the Operation Fixture.

For example a necessary validation to the transaction operation would be an ExistantAccountValidation.

Steps:
- Create the class
- Inherit the BusinessValidation
- Declare the ExistantAccountValidation in the TransactionOperation.InitializeFixture method

## Persistence
The persistence layer has two repositories one for each known Operation.
- AccountRepository
- TransactionRepository

This implementations handle the memory persistence. 


# Notes

Because of some eventualitys with my timings, the current tests implementation lacks the unit test of the domain layer but it should be developed with the DI pattern in mind. 

My approach would be to use the Moq library to help the development of the necessary tests. 
