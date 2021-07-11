# PremiumCalc
Soluntion Info:
This is a demo application, I have used .Net Core 3.1 to build up this POC. It is separated into 2 main project. Premium and PremiumApi. 
UI inputs are validated 2 ways, using Jquery and Data Annotations. 
As the API followed the Microservice pattern, api inputs have been validated using FluentAPI before using them.

Solution contain 4 project as below

1. Premium
	- This is the UI of the project build up in .net core 3.1. 
2. PremiumApi
	- This is the backend api build up in .net core 3.1. This Api receives an input model and return the premium value as a response model. 
3. PremiumTest
	- This is the test project for Premium. Xunit have been used to wirte tests.
4. PremiumApiTest
	- This is the test project for PremiumApi. Xunit have been used to wirte tests.

Steps to run:
1. Make sure to select Multiple startup projects option from Solution properties. Start Premium and PremiumApi project.
2. Click the Start, it should brings up 2 browser instace. one for the UI and another for the API. 
3. Enter the required field and click calculate button to get the premium amount. 
   You can also recalculate the premium by changing the Occupation Type dropdown.