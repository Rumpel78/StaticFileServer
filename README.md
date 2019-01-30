# StaticFileServer
## dotnet core project

This projects contains a bare static file delivery server with configurable base path to be used as a base docker image for simple web applications

## Usage:

### Dockerfile
Create a Dockerfile for your application:

```
FROM registry.gitlab.com/rumpel78/staticfileserver:latest
COPY ./files/ /app/wwwroot
```
Change the COPY line according to your static source files

### docker-compose.yml
Your docker-compose.yml should look like this one:
```
version: '2'

services:
  mysite:    
    build: .
    ports:
      - 80:80
    environment:
      - BASEPATH=/myBasePath
      - SPA_APPLICATION=true
```

`BASEPATH` sets the application base path, default is the domain root
`SPA_APPLICATION` determines wether to server `index.html` if no static file was found, instead of a 404 error. Useful for single-page-applications. Defaults to true.

Change the basepath according to your wishes. If you dont need a basepath, just remove the variable or keep it empty, don't add a trailing slash

### Why I built this image

Because I wanted some easy and performant solution of hosting some plain html/js files using Traefik Proxy and Path selector.