// C#
public class Solution {

    // [860] 柠檬水找零
    // 贪心法
    public bool LemonadeChange (int[] bills) {
        // 1.switch case
        if (bills.Length <= 1) return true;
        int m5 = 0, m10 = 0;
        foreach (int m in bills) {
            if (m == 5) {
                m5++;
            } else if (m == 10) {
                m10++;
                m5--;
            } else if (m == 20) {
                if (m10 > 0) {
                    m10--;
                    m5--;
                } else {
                    m5 -= 3;
                }
            }
            if (m5 < 0) return false;
        }
        return true;

        // 2.字典法
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        dic[5] = 0;
        dic[10] = 0;
        foreach (int m in bills) {
            if (m == 5) {
                dic[5]++;
            } else if (m == 10) {
                dic[10]++;
                dic[5]--;
            } else if (m == 20) {
                if (dic[10] > 0) {
                    dic[10]--;
                    dic[5]--;
                } else {
                    dic[5] -= 3;
                }
            }
            if (dic[5] < 0) return false;
        }
        return true;
    }

    // [122] 买卖股票的最佳时机 II
    // 贪心法
    public int MaxProfit (int[] prices) {
        int sum = 0;
        for (int i = 1; i < prices.Length; i++) { // 如果有利润，第一天买第二天抛
            int sub = prices[i] - prices[i - 1];
            if (sub > 0) sum += sub;
        }
        return sum;
    }

    // [455] 分发饼干
    // 贪心法
    public int FindContentChildren (int[] g, int[] s) {
        if (g.Length == 0 || s.Length == 0) return 0;
        Array.Sort (g);
        Array.Sort (s);
        //int i = 0;
        //int j = 0;
        //while (i < g.Length && j < s.Length)
        //{
        //    if (g[i] <= s[j])
        //    {// 满足
        //        i++;
        //        j++;
        //    }
        //    else
        //    {// 不满足
        //        j++;
        //    }
        //}
        //return i;

        int i = 0;
        for (int j = 0; i < g.Length && j < s.Length; j++) {
            if (g[i] <= s[j]) i++;
        }
        return i;
    }

    // [874] 模拟行走机器人
    public int RobotSim (int[] commands, int[][] obstacles) {
        int[] dx = new int[] { 0, 1, 0, -1 };
        int[] dy = new int[] { 1, 0, -1, 0 };
        int x = 0, y = 0, di = 0;

        // Encode obstacles (x, y) as (x+30000) * (2^16) + (y+30000)
        HashSet<long> obstacleSet = new HashSet<long> ();
        foreach (int[] obstacle in obstacles) {
            long ox = (long) obstacle[0] + 30000;
            long oy = (long) obstacle[1] + 30000;
            obstacleSet.Add ((ox << 16) + oy);
        }

        int ans = 0;
        foreach (int cmd in commands) {
            if (cmd == -2) //left
                di = (di + 3) % 4;
            else if (cmd == -1) //right
                di = (di + 1) % 4;
            else {
                for (int k = 0; k < cmd; ++k) {
                    int nx = x + dx[di];
                    int ny = y + dy[di];
                    long code = (((long) nx + 30000) << 16) + ((long) ny + 30000);
                    if (!obstacleSet.Contains (code)) {
                        x = nx;
                        y = ny;
                        ans = Math.Max (ans, x * x + y * y);
                    }
                }
            }
        }

        return ans;
    }

    // [127] 单词接龙
    public object LadderLength (string beginWord, string endWord, IList<string> wordList) {
        // 因为所有的单词都是一样长的。
        int len = beginWord.Length;

        // 字典保存可以组成的单词的组合，
        // 从任何给定的词。通过改变一个字母为*。
        // {中间词 : list<单词>}
        Dictionary<string, List<string>> allComboDic = new Dictionary<string, List<string>> ();

        foreach (string word in wordList) {
            for (int i = 0; i < len; i++) {
                // 中间词
                string middleWord = word.Substring (0, i) + "*" + word.Substring (i + 1);

                // Key是中间词
                // Value是具有相同中间词的单词列表。
                if (allComboDic.ContainsKey (middleWord)) {
                    allComboDic[middleWord].Add (word);
                } else {
                    allComboDic[middleWord] = new List<string> { word };
                }
            }
        }

        // Queue for BFS
        Queue<KeyValuePair<string, int>> queue = new Queue<KeyValuePair<string, int>> ();
        queue.Enqueue (new KeyValuePair<string, int> (beginWord, 1));

        // 已访问的字典-确保不重复处理相同的字。
        Dictionary<string, bool> visited = new Dictionary<string, bool> ();
        visited.Add (beginWord, true);

        // BFS
        while (queue.Count != 0) {
            KeyValuePair<string, int> curPair = queue.Dequeue ();
            string curWord = curPair.Key;
            int level = curPair.Value;
            for (int i = 0; i < len; i++) {
                // 当前单词的中间词
                string middleWord = curWord.Substring (0, i) + "*" + curWord.Substring (i + 1);

                // 反查-通过相同中间词找到对应单词。
                if (allComboDic.ContainsKey (middleWord)) {
                    foreach (string adjacentWord in allComboDic[middleWord]) {
                        // 如果我们在任何时候找到了我们要找的东西
                        // 近似词 == 结尾词，就可以返回答案。
                        if (adjacentWord.Equals (endWord)) {
                            return level + 1;
                        }
                        // 否则，将其添加到BFS队列，并且标记它为已访问。
                        if (!visited.ContainsKey (adjacentWord)) {
                            visited.Add (adjacentWord, true);
                            queue.Enqueue (new KeyValuePair<string, int> (adjacentWord, level + 1));
                        }
                    }
                }
            }
        }
        return 0;
    }

    // [200] 岛屿数量
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

    // [529] 扫雷游戏
    public int[] dirX = { 0, 0, -1, 1, -1, -1, 1, 1 };
    public int[] dirY = { 1, -1, 0, 0, 1, -1, -1, 1 };
    public char[][] UpdateBoard (char[][] board, int[] click) {
        int x = click[0];
        int y = click[1];
        // 规则1
        if (board[x][y] == 'M') {
            board[x][y] = 'X';
            return board;
        }
        // BFS
        Queue<int[]> queue = new Queue<int[]> ();
        bool[, ] visited = new bool[board.Length, board[0].Length];
        visited[x, y] = true;
        queue.Enqueue (click);
        while (queue.Count > 0) {
            int[] temp = queue.Dequeue ();
            int count = 0;
            // 先检查此点的周围 & 计数
            for (int i = 0; i < 8; i++) {
                int tempX = temp[0] + dirX[i];
                int tempY = temp[1] + dirY[i];
                if (tempX >= 0 && tempX < board.Length && tempY >= 0 && tempY < board[0].Length) {
                    if (board[tempX][tempY] == 'M') {
                        count++;
                    }
                }
            }

            if (count > 0) {
                // 规则3 & 计数后的节点不继续搜索
                //board[temp[0]][temp[1]] = char.Parse(count.ToString());
                board[temp[0]][temp[1]] = (char) (count + '0');
            } else {
                // 规则2 广度搜索连接未打开节点
                board[temp[0]][temp[1]] = 'B';
                for (int j = 0; j < 8; j++) {
                    int tempX = temp[0] + dirX[j];
                    int tempY = temp[1] + dirY[j];
                    if (tempX >= 0 && tempX < board.Length && tempY >= 0 && tempY < board[0].Length && board[tempX][tempY] == 'E' && !visited[tempX, tempY]) {
                        visited[tempX, tempY] = true;
                        queue.Enqueue (new int[] { tempX, tempY });
                    }
                }
            }
        }
        return board;
    }

    // [55] 跳跃游戏
    public bool CanJump (int[] nums) {
        int k = 0;
        for (int i = 0; i < nums.Length; i++) {
            if (i > k) return false;
            k = Math.Max (k, i + nums[i]);
        }
        return true;
    }

    // [33] 搜索旋转排序数组
    // 二分查找法
    public int Search (int[] nums, int target) {
        int left = 0, right = nums.Length - 1;
        int mid;
        while (left <= right) {
            mid = left + (right - left) / 2;
            if (nums[mid] == target) {
                return mid;
            }
            // 先根据 nums[mid] 与 nums[left] 的关系判断升序部分是在左段还是右段
            if (nums[mid] >= nums[left]) // 左半段为升序
            {
                // 再判断 target 是在 mid 的左边还是右边，从而调整左右边界 lo 和 hi
                if (target >= nums[left] && target < nums[mid]) {
                    right = mid - 1;
                } else {
                    left = mid + 1;
                }
            } else // 右半段为升序
            {
                if (target > nums[mid] && target <= nums[right]) {
                    left = mid + 1;
                } else {
                    right = mid - 1;
                }
            }
        }
        return -1;
    }
}