<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8" import="java.util.*, com.short_rpg.model.*" %>
<%
    Player player = (Player)request.getAttribute("player");
%>
<!DOCTYPE html>
<html lang="ja">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>プレイヤーランキング</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <!-- ヘッダー -->
    <header class="bg-dark text-white text-center py-3">
        <h1>プレイヤーランキング</h1>
    </header>
    <!-- メインコンテンツ -->
    <main class="container my-5">
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead class="table-dark">
                    <tr>
                        <th scope="col">順位</th>
                        <th scope="col">プレイヤー名</th>
                        <th scope="col">経験値</th>
                        <th scope="col">所持金</th>
                        <th scope="col">スコア</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row"></th>
                        <td><%= player.getName() %></td>
                        <td><%= player.getExperience() %></td>
                        <td><%= player.getMoney() %></td>
                        <td><%= player.getScore() %></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>
    <!-- フッター -->
    <footer class="bg-dark text-white text-center py-3">
        <p class="mb-0">&copy; 2024 勇者一同チーム.</p>
    </footer>
    <!-- Bootstrap JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
