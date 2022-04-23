# NewsMedia - Functional and Technical Overview

## Functionality:
* Registration of users
* Creation, edit, publish, list and delete reports. Can only delete reports they create.  
* Add comments to any report and update and delete any comments they have made. 
* Reports can be put into categories. The categories are defined and can be changed by the user. 


When using or updating the application, some important points to note: 

1. Only a registered user can access functionality in under Reports, Comments and Categories
2. A comment can only be created when in "edit" reports
3. A user can only delete reports they created
4. When a user deletes a report, all comments related to that report are also deleted


## Technical Considerations: 
* The application has been created using microsofts MVC and WebAPI Frameworks
* ViewBag was used to convert category id to the category name for display purposes 
* TempData was used to enable the comments "Create" function to know which reports to link comment 
* API search parameters were added to enable filtering of lists 
   1. Reports - All reports/logged in user reports and category
   2. Comments - all comments for a specific report/ all reports for a specific user 

Old code has been left in to show progression as we worked through solutions. 
This is technical debt to be removed at a later date. 
 






 



