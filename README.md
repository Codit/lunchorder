[![Visual Studio Team services](https://img.shields.io/vso/build/codit/af04086b-9d24-45cd-a1e6-8b7b65149a98/172.svg?maxAge=2592000?style=flat-square)]()
[![Travis Master branch](https://img.shields.io/travis/CoditEU/lunchorder/master.svg?maxAge=2592000?style=flat-square)](https://travis-ci.org/CoditEU/lunchorder)
[![Coveralls Master branch](https://img.shields.io/coveralls/CoditEU/lunchorder/master.svg?maxAge=2592000?style=flat-square)](https://coveralls.io/github/CoditEU/lunchorder)
[![Code Climate](https://img.shields.io/codeclimate/github/CoditEU/lunchorder.svg?maxAge=2592000?style=flat-square)](https://codeclimate.com/github/CoditEU/lunchorder)
[![David](https://img.shields.io/david/CoditEU/lunchorder.svg?maxAge=2592000?style=flat-square)](https://david-dm.org/CoditEU/lunchorder)

# lunchorder
Lunch order is a web application where a user can order lunch using money that was paid in advance. The order will then be delivered by a lunch company at lunchtime.
In essence, it should solve the following problems:
- Users that forget to pay for lunch
- Users that log the wrong payment amount
- User can order from a remote location
- The order will be sent to the lunch company on time
- Correct total payment amount will be known up front and ready on delivery.

The application is being been developed primarily to get a better understanding on how [angular2] works.

## Running the application
Please see the [wiki installation section]

Our local development is done in [Visual Studio Code] (frontend) and [Visual Studio 2015] (WebApi backend).

## Technology
Front-End:
- [angular2]
- [Bootstrap 3]
- [MomentJS]

Back-End:
- [ASP.NET Web Api]
- [DocumentDb]

## Demo:
https://www.lunchorder.be/ 

_Admin users_

username: demo_admin, password: demo_admin

username: demo_admin1, password: demo_admin1

...

username: demo_admin10, password: demo_admin10

_Normal users_

username: demo_user, password: demo_user

username: demo_user1, password: demo_user1

...

username: demo_user10, password: demo_user10


## License information
Lunchorder is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.

[angular2]: <https://github.com/angular/angular>
[Visual Studio Code]: <https://code.visualstudio.com/>
[DocumentDb]: <https://azure.microsoft.com/en-us/services/documentdb/>
[Bootstrap 3]: <http://getbootstrap.com/>
[MomentJS]: <https://github.com/urish/angular2-moment>
[Sass]: <http://sass-lang.com/>
[ASP.NET Web Api]: <http://www.asp.net/web-api>
[wiki installation section]: <https://github.com/CoditEU/lunchorder/wiki/Installation>
