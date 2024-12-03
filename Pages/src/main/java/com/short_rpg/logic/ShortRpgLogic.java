package com.short_rpg.logic;

import java.io.BufferedReader;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import org.json.JSONObject;

import com.short_rpg.model.Player;

import jakarta.servlet.http.HttpServletRequest;

public class ShortRpgLogic {

    // POSTリクエストを処理し、レスポンスを作成する
    public Player processRequest(HttpServletRequest request) throws Exception {
        // リクエストをStringBuilderで組み立て
        StringBuilder jsonBuilder = new StringBuilder();
        try (BufferedReader reader = request.getReader()) {
            String line;
            while ((line = reader.readLine()) != null) {
                jsonBuilder.append(line);
            }
        }

        // JSONデータをパース
        String jsonData = jsonBuilder.toString();
        JSONObject jsonObject = new JSONObject(jsonData);

        // Playerオブジェクトに変換
        Player player = new Player();
        player.setName(jsonObject.getString("player_name"));
        player.setExperience(jsonObject.getInt("experience"));
        player.setMoney(jsonObject.getInt("money"));
        player.setScore(calculateScore(player.getExperience(), player.getMoney()));

        return player;
    }

    // スコア計算ロジック
    private int calculateScore(int experience, int money) {
        return money * 2 + experience; // 例:所持金*2 + 経験値
    }
    
    // スコアの降順でリストをソート(DAO側で行うことにしたので削除予定)
    public List<Player> sortPlayersByScore(List<Player> players) {
        Collections.sort(players, Comparator.comparingInt(Player::getScore).reversed());
        return players;
    }
}
