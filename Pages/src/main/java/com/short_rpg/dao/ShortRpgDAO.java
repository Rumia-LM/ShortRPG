package com.short_rpg.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import javax.naming.NamingException;

import com.short_rpg.model.Player;

public class ShortRpgDAO {
	public Player getShortRpgById(int id) {
        String sql = "SELECT * FROM player_scores WHERE user_id = ?";
        Player player = null;
        try (Connection db = new ShortRpgDataSource().getConnection();
             PreparedStatement ps = db.prepareStatement(sql)) {

            ps.setInt(1, id);
            ResultSet rs = ps.executeQuery();

            if (rs.next()) {
            	player = new Player();
            	player.setId(rs.getInt("user_id"));
            	player.setName(rs.getString("player_name"));
                player.setExperience(rs.getInt("experience"));
                player.setMoney(rs.getInt("money"));
                player.setScore(rs.getInt("score"));
            }

        } catch (SQLException | NamingException e) {
            e.printStackTrace();
        }
        return player;
    }
}