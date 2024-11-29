package com.short_rpg.dao;

import java.sql.Connection;
import java.sql.SQLException;

import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;

public class ShortRpgDataSource {
	public Connection getConnection() throws NamingException, SQLException {
		InitialContext ctx = new InitialContext();
		DataSource ds = (DataSource)ctx.lookup("java:comp/env/jdbc/short_rpg");
		return ds.getConnection();		
	}
}