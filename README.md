# typicode-data-transformation

## How to Run
1. You may have to configure a certificate:
   a. Create a certificate with 'dotnet dev-certs https'
   b. Run 'dotnet dev-certs https --trust' to use the dev certificate you created

2. Use Swagger for the api directly. Or the UI when it is fully implemented.

## Comments
Due to the time box I focused on the backend api and best practices there. It's been a while since I did any frontend so it would not be a trivial thing to do. In terms of picking up to a prod-ready implementation, I started looking into this link to consume the api and do the listings in the web project: https://www.mikesdotnetting.com/article/261/integrating-web-api-with-asp-net-razor-web-pages

I believe that enough insight can be gained from the current implementation but here are a couple of other resources that have more in-depth details:
- https://github.com/mteknight/beerquest-engineering-challenge
- https://github.com/mteknight/pokemon-shakespeare-translator

## Next steps
1. Consume the api from the UI in the Web project and list the data.
2. Typicode config such as their base url should go into an options pattern injected into the service.
3. Service and Domain should be moved to their respective projects (not together as the service is not a domain one).
4. Given more time, additional tests should be added to handle external service unavailable or missing data.
