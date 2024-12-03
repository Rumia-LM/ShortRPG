package com.short_rpg.controller;

import java.io.IOException;
import java.util.List;

import com.short_rpg.dao.ShortRpgDAO;
import com.short_rpg.model.Player;

import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@WebServlet("/DeletePlayerServlet")
public class DeletePlayerServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String playerName = request.getParameter("playerName");
		ShortRpgDAO dao = new ShortRpgDAO();
		dao.deletePlayer(playerName);
		List<Player> players = dao.getAllPlayerSortedByScore();
        request.setAttribute("playerList", players);
        request.getRequestDispatcher("/WEB-INF/view/ShortRpgRanking.jsp").forward(request, response);
	}
}
