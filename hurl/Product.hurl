
### List all
GET http://localhost:5269/api/Products
Accept: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiI4NTVhYjViYzhmNjM0NmQxYmI3MTRkNDQxNDVjY2MyYyIsInN1YiI6Im5pY2VzODg4IiwiYXVkIjpbIlRoZSBBdWRpZW5jZSIsIjg1NWFiNWJjOGY2MzQ2ZDFiYjcxNGQ0NDE0NWNjYzJjIl0sImV4cCI6MTcyMjI0NjQ0MywibmJmIjoxNzIyMTYwMDQzLCJpYXQiOjE3MjIxNjAwNDMsImp0aSI6ImNlOTRmMTE2NTk5NDQ5NDZhYTQxYmU3ZDRkNGYwY2JkIiwibmFtZWlkIjoibmljZXM4ODgiLCJ1bmlxdWVfbmFtZSI6Im5pY2VzODg4IiwiVGltZXN0YW1wIjoiMTcyMjE2MDA0MzA5NCIsIlNlcmlhbE5vIjoiY2U5NGYxMTY1OTk0NDk0NmFhNDFiZTdkNGQ0ZjBjYmQiLCJPcGVyYXRvcklEIjoiODg5IiwiQWNjb3VudCI6Im5pY2VzODg4IiwiTmFtZSI6Im5pY2VzODg4IiwiRW1haWwiOiJuaWNlczg4OEBuaWNlcy50ZWNobm9sb2d5In0.xS6jMKPQ2knoOY4a03ckx89Tg-H2B_LcQmhlr29dDlM

### list all products
GET http://localhost:5269/api/Products
Accept: application/json

### Create
POST http://localhost:5269/api/Products
Accept: application/json

{
  "name": "FooBar",
  "price": 10.0
}

### Get By Id
GET http://localhost:5269/api/Products/1
Accept: application/json

### Not Found 404
GET http://localhost:5269/api/Products/111
Accept: application/json

### Not valid value 400
GET http://localhost:5269/api/Products/aaa
Accept: application/json

### delete 
DELETE http://localhost:5269/api/Products/10
Accept: application/json
