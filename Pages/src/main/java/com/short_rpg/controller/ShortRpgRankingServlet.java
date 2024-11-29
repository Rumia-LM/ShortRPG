package com.short_rpg.controller;

import java.io.IOException;

import com.short_rpg.dao.ShortRpgDAO;
import com.short_rpg.model.Player;

import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@WebServlet("/ShortRpgRankingServlet")
public class ShortRpgRankingServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		int id = 1;

        ShortRpgDAO shortRpgDAO = new ShortRpgDAO();
        Player player = shortRpgDAO.getShortRpgById(id);
        request.setAttribute("player", player);
		request.getRequestDispatcher("WEB-INF/view/ShortRpgRanking.jsp").forward(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		doGet(request, response);
	}
}
