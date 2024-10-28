# ProductCatalog
 
This project is a simple web application built with ASP.NET Core using Clean Architecture principles and .NET 8. The application provides CRUD operations for Products, Categories, Subcategories, and Brands, with Dapper used for data access. The frontend uses the AdminLTE Bootstrap template to deliver a responsive UI.

Features
- Product Management: View, add, edit, and delete products with details such as name, price, category, subcategory, and brand.
  Category, Subcategory, and Brand Management: Manage related data entities to categorize products effectively.
- Responsive UI: Developed using Bootstrap and AdminLTE for a modern, user-friendly interface.
- Logging: Integrated logging for debugging and monitoring.
- Encryption: Data encryption is implemented to secure sensitive data.

Technologies Used
- Backend: ASP.NET Core (.NET 8)
- Frontend: Bootstrap, AdminLTE Template, jQuery
- Data Access: Dapper
- Database: MSSQL Server
- Logging: Custom logging setup
- Encryption: Data encryption for sensitive information

# Getting Started
Prerequisites
- .NET 8 SDK
- SQL Server

Installation Steps
1. Clone the Repository:
   <a href="https://github.com/AungMyintMyat-30/ProductCatalog">Repository URL</a>
2. Set Up the Database:
   <a href="https://1drv.ms/u/s!AinuagkfRoffgukjOP9e9Ha_rw9Pow?e=CmCvSj">Download</a> Database file from this link.<br>
   The .bak file cannot be restored to earlier versions of SQL Server (e.g., SQL Server 2014, SQL Server 2012).<br>
   The .bak file created in MSSQL Server 2016 can be restored to:
   - Any instance of SQL Server 2016.
   - Any later version of SQL Server (e.g., SQL Server 2017, SQL Server 2019).<br>
   Update the connection string in appsettings.json
   ### Example JSON Configuration
   {
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=your_server;Initial Catalog=ProductCatalog;Persist Security Info=True;User ID=sa;Password=your_password;TrustServerCertificate=True"
  },
}
3. Restore Packages and Build the Project.
4. Run the Application.
