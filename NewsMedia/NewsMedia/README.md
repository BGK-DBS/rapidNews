# News Report Creation
Open Visual Studio Create a new ASPO.NET Core Web App(Model-View-Controller)

Name Given: NewsMedia

Select NET 6.0 and Authentication Type: Individual Accounts.

#Create a Model  
	1- Open Visual Studio 22  
	2- Select Add  
	3- Select New Folder and add NewsReport Model  
	4- Right click on Models folder and add  new Class called NewsReport  

#Scafolding  
		1- Click Controller folder  
		2- Add "Microsoft.Entity.Design, select version based om sdk  
		3- Select Add New Scafolding item  
		4- Select the APS.NET  with MVC  
		5- Select  NewsReport Model  
		6-Select + icon for adding a new context  
		7 - Use default cotroller name  

#DropDownList  
	1-Get Category Model  
	2-Create a Viewbag from Category Model  
	3-Get Category elements from _context.Category.ToList();  
	4-Add Viewbag to:  
  	4.1 Create View  
  	4.2 Details View  
  	4.3 Edit View  
  	4.4 Delete View  

#Managing Data With ViewModels  
	1- Create a new Controller called NewsReportViewModel  
	2- Scafolding the new controller NewsReportViewModel  
	3- Go to Idenx on the NewsReport controller  
  	3.1 - Get newsReport from the WebAPI  
  	3.2 - Get  NewsReportViewModel from the MVC  
        4- Call the drowpdown list (viewbags)  
        4.1 Validate Category Name by ID  
        4.2 If CategoryID exist brings CategoryName from CategoriesModel  
   
#
# Report
Rapid News Reports Web API

Notes as of 29/03 : 
Uses in Memory Db

Post - set date modified date = date created 

Put - Created date should not be modified  

Swagger set-up so can test by running ctrl F5 and putting /swagger/index.html
