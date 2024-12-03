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

@WebServlet("/ShortRpgRankingServlet")
public class ShortRpgRankingServlet extends HttpServlet {
    private static final long serialVersionUID = 1L;

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException, ServletException {
        ShortRpgDAO dao = new ShortRpgDAO();

        // DAOから全プレイヤーリストを取得
        List<Player> players = dao.getAllPlayerSortedByScore();

        // JSPにリストを渡す
        request.setAttribute("playerList", players);
        request.getRequestDispatcher("/WEB-INF/view/ShortRpgRanking.jsp").forward(request, response);
    }
}
