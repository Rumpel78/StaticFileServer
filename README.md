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
      - ASPNETCORE_BASEPATH=/myBasePath
      - ASPNETCORE_SPA=true
      - ASPNETCORE_INDEX=index.html
```

- `ASPNETCORE_BASEPATH` sets the application base path, default is the domain root  
- `ASPNETCORE_SPA` determines wether to server the index file if no static file was found, instead of a 404 error. Useful for single-page-applications. Defaults to true.  
- `ASPNETCORE_INDEX` defines the the index file to search in case of `ASPNETCORE_SPA` is set to true. Default is `index.html`

You can omit all environment variables and use the default values (basepath is root, server index.html in case of 404 not found)

### Why I built this image

Because I wanted some easy and performant solution of hosting some plain html/js files using Traefik Proxy and Path selector.