package com.short_rpg.controller;

import java.io.IOException;

import com.short_rpg.logic.ShortRpgLogic;
import com.short_rpg.model.Player;

import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@WebServlet("/JsonServlet")
public class JsonServlet extends HttpServlet {
    private static final long serialVersionUID = 1L;

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
    	Player player = new Player();
        ShortRpgLogic logic = new ShortRpgLogic();

        try {
            // Logicクラスでリクエストを処理し、プレイヤーインスタンスを生成
            player = logic.processRequest(request);
            
        } catch (Exception e) {
            e.printStackTrace();
            response.setStatus(HttpServletResponse.SC_BAD_REQUEST);
            response.getWriter().write("{\"error\": \"Invalid request\"}");
        }
        
        if(player!=null) {
        	if(logic.nameCheck(player)) {
        		System.out.println("すでに存在するプレイヤーのデータを更新しました");
        	}else {
        		System.out.println("プレイヤーを新しくデータベースに登録しました");
        	}
        }
    }
}
