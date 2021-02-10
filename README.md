# GrapeCityBlog
-----------------------------------------------------------
How To Run

1.  Update-Database

----------------------------------------------
Default Role
1. Admin
2. User
------------------------------------------------------------------------------------------------------------------------
Default Users

1.   username : AAAAA

     password : Helloworld_007
     
     role :  admin
     
     
2.   username : BBBBB

     password : Helloworld_007
     
     role :  user
     
     
     
3.   username : CCCCC

     password : Helloworld_007
     
     role :  user
     
     
     -------------------------------------------------------------------------------------------------------------
     JWT Authentication
     Steps
     1. Login get token
     2. Send Token with every request in header
     
     -------------------------------------------------------------------------------------------------------------
     Database
     SQL Server
     -> Provide connection string in appsettings
     
         "ConnectionStrings":
          {
               "sqlConnection": "Server=INGURWN120084\\SQLEXPRESS01;Database=GrapeCityDB;Trusted_Connection=True;"
          }
     
     -------------------------------------------------------------------------------------------------------------
     URLS
     
     
     1. GET    : https://localhost:44386/api/Blogs             ---------- GET     all blog
     2. GET    : https://localhost:44386/api/Blogs/{blogId}    ---------- GET     Perticular blog
     3. DELETE : https://localhost:44386/api/Blogs/{blogId}    ---------- DELETE  Perticular blog
     4. POST   : https://localhost:44386/api/Blogs/{blogId}    ---------- POST    Perticular blog
          
       JSON INPUT : {
                        "title": "TITLE",
                        "text": "TEXT"
                       }
                       
     5. PUT    : https://localhost:44386/api/Blogs/{blogId}    ---------- PUT     Perticular blog
     
              JSON INPUT : {
                        "title": "TITLE",
                        "text": "TEXT"
                       }
                       
                       
   ----------------------------------------------------------------------------------------------------------------                    
