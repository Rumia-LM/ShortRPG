package com.short_rpg.model;

public class Player {
    private int id;
    private String name;
    private int experience;
    private int money;
    private int score;
    private int play_count;
    
    public Player() {} //空のコンストラクタ・オーバーロード

    public Player(int id, String name, int experience, int money, int score, int play_count) {
        this.id = id;
        this.name = name;
        this.experience = experience;
        this.money = money;
        this.score = score;
        this.play_count = play_count;
    }

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getExperience() {
		return experience;
	}

	public void setExperience(int experience) {
		this.experience = experience;
	}

	public int getMoney() {
		return money;
	}

	public void setMoney(int money) {
		this.money = money;
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public int getPlay_count() {
		return play_count;
	}

	public void setPlay_count(int play_count) {
		this.play_count = play_count;
	}
       
}
