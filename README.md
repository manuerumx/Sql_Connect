Sql_Connect
===========
C# Class for making SQL Connections strings

Visual Studio 2010 C# class

About
=====
A simple class for making SQL Connections strings. 

Example

    Sql_Connect sqlcnn = new Sql_Connect();	
	sqlcnn.DBUser = "sa";
	sqlcnn.DBPass = "myPassword";
    sqlcnn.DBServer = "(local)";
    sqlcnn.DBName = "TempDB";
	string myStringConn = sqlcnn.Make_String();