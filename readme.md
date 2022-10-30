# Weather API
This is a very useful C# solution to provide the Weather, Geo and Air quality information for an inputted location, using third party APIs and does the following:
- keeps the user aware of air quality regarding pollen and particulate matter in air
- suggests whether the user can go out based on the air quality of the inputted location
- provides the user the current weather, weekly forecast and hourly feels like temperature forecast
- suggests what clothing the user can wear based on the weather forecasts
- also finds the latitude and longitude details for the inputted location

## Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [Installing](#installing)
- [Usage](#usage)
- [Contributing](#contributing)

## About

When we plan for a day/ days outside, we may have to consider certain factors like weather and air quality in order to have a pleasant and enjoyable trip.
#### This Weather API project is developed to provide a wonderful solution by providing suggestions for the below end-users,
- for those who are planning for their day outside (to be cautious about what they wear)
- for those who are allergic to pollens (hay fever) 
- for people who have asthma, rhinoconjunctivitis and atopic dermatitis (problems associated with airways)
- for those who need to know their current location coordinates

The endpoints used in this application are:
### Air Quality:
#### 1. Suggestions based on Pollen in air:

![image](https://user-images.githubusercontent.com/111776991/198899484-a72a7884-e845-4124-bdde-be6b222c6100.png)

###### Kateryna

#### 2. Suggestions based on Particulate Matter:

![image](https://user-images.githubusercontent.com/111776991/198899531-76e7e818-e0f1-4b42-8f06-d64e12428b96.png)

###### Kenny

### Geo:

![image](https://user-images.githubusercontent.com/111776991/198899600-abea6789-bf9b-4aca-b712-905a148e233d.png)

###### Shahzaib


### Weather:

#### 1. Suggestions based on Weekly Forecast:

![image](https://user-images.githubusercontent.com/111776991/198899668-25887d42-1621-4a5e-b255-3b4240a407f2.png)

###### Prasanna


#### 2. Suggestions based on Hourly Feels Like Temperature Forecast for the current day:

![image](https://user-images.githubusercontent.com/111776991/198899888-20e6e1fe-c775-45e4-b855-a4de8a2d3d79.png)

- The 'feels like' temperature is different to the actual air temperature shown on a weather forecast. The 'feels like' temperature measures the expected air temperature, relative humidity and the strength of the wind at 5 feet (human height) as well as an understanding of how heat is lost from the human body during cold and windy days.
- 'Feels like' temperatures throughout the year are particularly influenced by wind. An example of this is in winter when winds blowing to the UK from a north- easterly direction make the 'feels like' temperatures colder than the actual air temperature. On a calm day, our bodies insulate us with a boundary layer which warms the air closest to the skin. If it is windy, the wind will take the boundary layer away and the skin temperature will drop making us feel colder.
- When the wind speed is low in periods of high temperatures, the 'feels like' temperatures become more impacted by the humidity level. When a human perspires, the water in the sweat evaporates leading to the cooling of the body as the heat is carried away. When humidity is high, this evaporation reduces resulting in 'feels like' temperatures that appear warmer than the actual air temperature.


#### 3. Suggestions based on Current Weather:

![image](https://user-images.githubusercontent.com/111776991/198899668-25887d42-1621-4a5e-b255-3b4240a407f2.png)

###### Shahzaib



The third party APIs used in this application to serve above aspects are
1. https://open-meteo.com/en/docs/air-quality-api
2. https://open-meteo.com/en/docs
3. https://open-meteo.com/en/docs/geocoding-api

## Pre-requisites

We need the following to be installed in order to use this application
- Visual Studio 2022 on Windows
- .NET 6.0 installed

## Instructions/ Usage

To get started,
- please "fork" this repository into your own Git account 
- then clone the repository to your machine
- Repository Link: https://github.com/2022-checkout-api-a-team-alpha-team/WeatherAPI

To run the application,
- After cloning, open the solution file in Visual Studio 2022, then press F5 or click Run.
- The application will run and open in swagger, where different endpoints could be seen and used by inputting a location.

To test the application,
- the tests could be run using the git commands 

```dotnet restore```

followed by 

```dotnet test```


When you run the application, the swagger will open and show the different endpoints as shown below.

![image](https://user-images.githubusercontent.com/111776991/198898537-03b5530b-29c7-4d75-bb2d-ecaf184eab80.png)


###### Kateryna - Pollen details
###### Kenny - Particulate Matter details
###### Prasanna - Weekly Forecast


### GET Hourly Weather Forecast - based on Feels Like Temperature

If you are planning to go out and you would like to get suggestions based on Feels Like Temperature for the current day to dress up accordingly, then
click on the endpoint as shown below.
![image](https://user-images.githubusercontent.com/111776991/198898848-b274a2c3-54b9-4925-8182-ad9f45612b5d.png)

Then the below details will be seen.

![image](https://user-images.githubusercontent.com/111776991/198899078-ba69f20f-8f9d-4e10-9576-d4027f610f90.png)


When you click on the button ![image](https://user-images.githubusercontent.com/111776991/198898895-e22e78f5-ce7d-4330-8ac4-24cf8ef03af3.png)
the text box for cityName will be anabled. Enter the City Name (Location) as input in the textbox and then click the Execute button.

![image](https://user-images.githubusercontent.com/111776991/198899118-6b975b49-c212-46a5-93d7-c59999ce95e8.png)

This API endpoint will return the results as shown in the below figure.

![image](https://user-images.githubusercontent.com/111776991/198899185-5dc84150-c8c9-44e3-a165-6ba2e401bae2.png)


###### Shahzaib - current Weather and suggestion details



## Contributing
Contributors names:

###### Kateryna
###### Prasanna
###### Sabitha
###### Shahzaib
###### Kenny
