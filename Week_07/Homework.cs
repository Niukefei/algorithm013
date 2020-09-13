// C#
public class Solution {

    // 70. 爬楼梯
    public int ClimbStairs (int n) {
        int prepre = 0; // 前前个台阶的走法
        int pre = 0; // 前个台阶的走法
        int sum = 1; // 当前台阶的总走法
        for (int i = 0; i < n; i++) {
            prepre = pre; // 更新前前
            pre = sum; // 更新前
            sum = pre + prepre; // 当前台阶的走法
        }
        return sum;
    }

    // 208. 实现 Trie (前缀树)
    class Trie {
        private bool IsEnd { get; set; }
        private Trie[] Nodes { get; } = new Trie[26];
        /** Initialize your data structure here. */
        public Trie () {

        }

        /** Inserts a word into the trie. */
        public void Insert (string word) {
            Trie node = this;
            foreach (var a in word) {
                if (node.Nodes[a - 'a'] == null)
                    node.Nodes[a - 'a'] = new Trie ();
                node = node.Nodes[a - 'a'];
            }
            node.IsEnd = true;
        }

        /** Returns if the word is in the trie. */
        public bool Search (string word) {
            Trie node = this;
            foreach (var a in word) {
                node = node.Nodes[a - 'a'];
                if (node == null)
                    return false;
            }
            return node.IsEnd;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith (string prefix) {
            Trie node = this;
            foreach (var a in prefix) {
                node = node.Nodes[a - 'a'];
                if (node == null)
                    return false;
            }
            return true;
        }
    }

    // 547. 朋友圈
    public int FindCircleNum (int[][] M) {
        if (M == null || M.Length <= 0 || M[0].Length <= 0) {
            return -1;
        }

        int len = M.Length;

        bool[] visited = new bool[len];

        int count = 0;

        for (int i = 0; i < len; ++i) {
            if (!visited[i]) {
                dfs (M, visited, i);
                count++;
            }
        }

        return count;
    }

    private void dfs (int[][] M, bool[] visited, int id) {
        for (int i = 0; i < M.Length; ++i) {
            if (M[id][i] == 1 && !visited[i]) {
                visited[i] = true;
                dfs (M, visited, i);
            }
        }
    }

    // 200. 岛屿数量
    public int[] dirX = { 1, -1, 0, 0 };
    public int[] dirY = { 0, 0, 1, -1 };
    public int row;
    public int cul;
    public int NumIslands (char[][] grid) {
        row = grid.Length;
        if (row == 0) return 0;
        cul = grid[0].Length;

        int count = 0;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < cul; j++) {
                if (grid[i][j] == '1') {
                    ++count;
                    grid[i][j] = '0';
                    Bfs (grid, i, j); // 对为'1'的岛屿广度递归
                }
            }
        }
        return count;
    }
    public void Bfs (char[][] grid, int x, int y) {
        Queue<int[]> queue = new Queue<int[]> ();
        queue.Enqueue (new int[] { x, y });
        while (queue.Count != 0) {
            int[] pos = queue.Dequeue ();
            // 遍历四周
            for (int i = 0; i < 4; i++) {
                int nx = pos[0] + dirX[i];
                int ny = pos[1] + dirY[i];
                if (nx >= 0 && nx < row && ny >= 0 && ny < cul && grid[nx][ny] == '1') {
                    grid[nx][ny] = '0';
                    queue.Enqueue (new int[] { nx, ny });
                }
            }
        }
    }

    // 130. 被围绕的区域
    // DFS 深度优先搜索 递归法
    public void Solve (char[][] board) {
        if (board == null || board.Length == 0) return;
        int m = board.Length; // 列
        int n = board[0].Length; // 行
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                bool isEdge = i == 0 || j == 0 || i == m - 1 || j == n - 1; // 边界？
                if (isEdge && board[i][j] == 'O') { // 对边界的'O'深度优先搜索
                    DFS (board, i, j);
                }
            }
        }
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (board[i][j] == '0') board[i][j] = 'X';
                if (board[i][j] == '#') board[i][j] = 'O';
            }
        }
    }
    // 递归深度优先搜索
    private void DFS (char[][] board, int i, int j) {
        if (i < 0 || j < 0 || i >= board.Length || j > board[0].Length || board[i][j] == 'X' || board[i][j] == '#') { // 终止条件(超边界，或者不是O，或者已经搜索过)
            return;
        }
        board[i][j] = '#';
        DFS (board, i - 1, j); // 上
        DFS (board, i + 1, j); // 下
        DFS (board, i, j - 1); // 左
        DFS (board, i, j + 1); // 右
    }

    // 22. 括号生成
    public IList<string> GenerateParenthesis (int n) {
        List<string> ans = new List<string> ();
        // 想象每半个括号放到一个格子中，一共有2n个格子
        // 先将括号都放入格子中，一共有多少种放法？
        // 再判断括号的合法性，用栈来检测
        Generate (0, 0, n, "", ans);
        return ans;
    }
    private void Generate (int left, int right, int n, string s, List<string> ans) {
        // terminator 终止条件
        if (left == n && right == n) { // 左和右都已经用完了
            ans.Add (s);
            return;
        }

        // process 处理当前层逻辑
        // drill down 下探到下一层
        if (left < n)
            Generate (left + 1, right, n, s + '(', ans);
        if (right < left)
            Generate (left, right + 1, n, s + ')', ans);

        // reverse status 清理当前层
    }
}