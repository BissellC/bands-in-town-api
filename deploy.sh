docker build -t bands-in-town-api-image .

docker tag bands-in-town-api-image registry.heroku.com/bands-in-town-api/web

docker push registry.heroku.com/bands-in-town-api/web

heroku container:release web -a bands-in-town-api