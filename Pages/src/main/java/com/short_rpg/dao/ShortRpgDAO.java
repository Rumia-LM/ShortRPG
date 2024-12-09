package com.short_rpg.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import javax.naming.NamingException;

import com.short_rpg.model.Player;

public class ShortRpgDAO {
	
	//　プレイヤーの一覧をスコア昇順で取得
	public List<Player> getAllPlayerSortedByScore(){
		List<Player> playerList = new ArrayList<>();
		String sql = """
					SELECT user_id, player_name, experience, money, score, play_count
					FROM player_scores
					ORDER BY score ASC
					 """;
				 
		try(Connection db = new ShortRpgDataSource().getConnection();
			PreparedStatement ps = db.prepareStatement(sql);
			ResultSet rs = ps.executeQuery()){
		
			while(rs.next()){
				Player player = new Player();
				player.setId(rs.getInt("user_id"));
				player.setName(rs.getString("player_name"));
				player.setExperience(rs.getInt("experience"));
				player.setMoney(rs.getInt("money"));
				player.setScore(rs.getInt("score"));
				player.setPlay_count(rs.getInt("play_count"));
				playerList.add(player);
			}
		}catch(SQLException | NamingException e){
			e.printStackTrace();
		}
		return playerList;
	}
	
    public boolean insertPlayer(Player player) {
        String sql = "INSERT INTO player_scores (player_name, experience, money, score) VALUES (?, ?, ?, ?)";
        try (Connection conn = new ShortRpgDataSource().getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, player.getName());
            stmt.setInt(2, player.getExperience());
            stmt.setInt(3, player.getMoney());
            stmt.setInt(4, player.getScore());
            return stmt.executeUpdate() > 0;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
	
	// プレイヤー名を指定して一件更新
    public boolean updatePlayer(Player player) {
        String sql = """
        			UPDATE player_scores
        			SET experience = ?,
        				money = ?,
        				score = ?,
        				play_count = ?
        				
        			WHERE player_name = ?
        			""";
        try (Connection db = new ShortRpgDataSource().getConnection();
             PreparedStatement ps = db.prepareStatement(sql)) {

            ps.setInt(1, player.getExperience());
            ps.setInt(2, player.getMoney());
            ps.setInt(3, player.getScore());
            ps.setInt(4, player.getPlay_count());
            ps.setString(5, player.getName());
            return ps.executeUpdate() > 0;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return false;
    }
	
	// プレイヤー名を指定して一件削除
	public void deletePlayer(String name){
		String sql = "DELETE FROM player_scores WHERE player_name = ?";
		try (Connection db = new ShortRpgDataSource().getConnection();
			PreparedStatement ps = db.prepareStatement(sql)){
			ps.setString(1,name);
			ps.executeUpdate();
		}catch (SQLException | NamingException e){
			e.printStackTrace();
		}
	}
	
	// プレイヤーの名前を指定して一件取得
	public Player getShortRpgByName(String name) {
        String sql = "SELECT * FROM player_scores WHERE player_name = ?";
        Player player = null;
        try (Connection db = new ShortRpgDataSource().getConnection();
             PreparedStatement ps = db.prepareStatement(sql)) {

            ps.setString(1, name);
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