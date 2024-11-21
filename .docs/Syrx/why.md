# Table of Contents
1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
   1. [Requirements](#requirements)
   2. [Installation](#installation)
3. [Features](#features)
4. [Usage](#usage)
   1. [Basic Usage](#basic-usage)
   2. [Advanced Usage](#advanced-usage)
5. [Contributing](#contributing)
6. [License](#license)

## Introduction
This is the introduction section.

## Getting Started
This is the getting started section.

### Requirements
These are the requirements.

### Installation
This is the installation section.

## Features
This is the features section.

## Usage
This is the usage section.

### Basic Usage
This is the basic usage section.

### Advanced Usage
This is the advanced usage section.

## Contributing
This is the contributing section.

## License
This is the license section.



# Why? 
Syrx was inspired by, and continues to use, [Dapper](https://github.com/DapperLib/Dapper). It was orginally called _[Drapper]([Dapper](https://github.com/DapperLib/Dapper))_ as a "wrapper for Dapper". 

While Dapper itself is the what is used under the hood with Syrx, the original author of Syrx was unhappy with a couple of the features of Dapper and wanted to write an abstraction to make it easier to use. As such, Syrx can be considered an opinionated framework as it reflects the opinions of its author. 

The primary driver for writing Syrx was to separate the SQL and C# code which is so often seen together with Dapper. It's the opinion of the Syrx author that this is messy and hurts readability.

Syrx separates the SQL and C# allowing you the flexibility of changing your underlying SQL without having to alter the C# class. In fact, using Syrx you can switch to an entirely different RDBMS without ever having to touch your C# code at all. This has enormous benefits for testability, reliability and delivery of higher quality code. 

Moreover, using Syrx you can easily support making calls to entirely different databases/instances in the same method. 


# The Name
The name Syrx is taken from the projects two original authors - David Sexton (S..x) and George Dyrrachitis (yr). I wanted a name that was short and punchy so that it was easy to type into the nuget CLI.


# Why should I use Syrx?
Syrx is for control freaks - people who like to have fine-grained control of what gets executed and how. It's for people who have very complex object graphs and want a simple way to both persist and retrieve them. 

Syrx allows you to write the same repository code to target multiple different databases. 
It is a framework which decouples your .NET repository code from the underlying data store, allowing you to shield your 
repository code from the underlying storage provider. It's not quite an ORM although it uses a micro-ORM to materialize and persist your objects.

Syrx places emphasis on:
* _Control_: You should be in control of your data and execution. 
* _Speed_: Performance is a feature. Syrx inherits its speed from Dapper.
* _Flexibilty_: Your choice of database technology should be as easy to change as any other component in your solution. 
* _Testability_: We believe strongly in the power of testing at all levels of your applications. Syrx is fully testable and fully tested.
* _Extensibility_: Syrx is granular and componentised, allowing you two swap out components for your own needs. 
* _Readability_: Syrx aims to keep the intent of your code clear and concise. 
