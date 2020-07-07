## Applitools Hackathon 2020

The challenge is to build the automation suite for the first version of the app and use it to find bugs in the second version (V2) of the app.

You need to automate three (3) main tasks across seven (7) different combinations of browsers and screen resolutions (viewports). 
Further, you need to automate the tasks in both the traditional approach and the Modern approach through Visual AI, for both V1 and V2 versions of the app. 
By “traditional approach”, we mean without using Applitools Visual AI. 
You can execute the traditional tests either locally or by using other cross-browser cloud solutions that you are already familiar with.

More details about the hackathon can be found https://applitools.com/cross-browser-testing-hackathon-v20-1-instructions/

## Overview

I chose to complete just the Modern approach using the Ultra Fast Grid

I used C# as that is the language I am most familar with.

## Packages

The project uses the following packages:

* Selenium Web Driver v3.141.0
* ChromeDriver v83.0.4103.3910 (requires Chrome browser v83)
* NUnit v3.12.0
* NUnit3TestAdapter v3.16.1 
* Eyes.Selenium v2.29.0


## Project Structure

* The VisualAI tests are in the VisualAITests class and utilize Applitools Eyes SDK  
	* The tests are in the ModernVisualAITests folder
	* A class that holds the locators for the application under test is in the Pages folder
	* The visual bugs are all documented in the Applitools Test Manager
* The version the tests execute against can be changed via variables utilizing the configuration manager:
	* ConfigurationManager.AppSettings["V1"]
	* ConfigurationManager.AppSettings["V2"]

## Pre-requisites

* Windows OS
* Visual Studio
* Chrome 83

## Installation

* Clone the repo
* Add your applitools api key to an environment variable named 'APPLITOOLS_API_KEY'
* Build the solution
* Execute the tests using the Visual Studio Test Explorer
	* Change the class variable private static readonly string _url in ModernVisualAITests.cs to switch between V1 and V2

## Author
* Mary Jo Zervas
