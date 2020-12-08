# Lecture 11 - Angular frontend for .NET backend
Erik Macháček
(erik.machacek@solarwinds.com)

## Description
Provided project is frontend for CookBook written in Angular. And it's purpose is to show possibility to use Angular for given task.

This project should be viewed as Sample App or Experimental. It is definitely not finished product which could be used in production.

I tried to use as basic constructions as possible to give you source for comparison between Blazor website and Angular one. Also this project should not be used as Angular tutorial as it is not.

## Project folders
This lecture contains two projects CookBook & CookBook_backend.

CookBook_backend contains copy of main lecture project from lab exercise 06 after which is version against whole front-end was build. It is copied here for compatibility reasons.

CookBook contains Angular frontend solution (all source code and additional files required for build, except modules - which needs to be downloaded by command shown bellow).

## Angular frontend
Project is Sample App and should be viewed as one. Which in this case means that it is usable but not ready for deployment to production. I used as little outside modules from Angular as possible, one exception is ng-openapi-gen used for API generation. I tried to avoid any complex (but more powerful) concepts and constructs so it is easier to understand (without specialized knowledge in given field).

### Requirements to run this project:
- IDE - VS Code is recommended
- Node.js (v14.15.) - standard installation https://nodejs.org/en/
- NPM (6.14.) - provided by Node.js
- Yarn (1.21.1) - installation through npm https://yarnpkg.com/getting-started/install

### How to run project:
All commands bellow (except backend) can be run from VS Code build-in terminal or PowerShell
- yarn install - one time setup which will download required packages
- ng serve - will provide live version of website on http://localhost:4200/
- ng test - will run Unit test session
- ng e2e - will run E2E test session (requires Chrome version 87)
- *backend* - can be run in standard manner (I always provide it by running debug on CookBook.Api projected, hosted on IIS Express)