// C#
public class Solution {

    // [64] 最小路径和
    // 动态规划
    public int MinPathSum (int[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;
        int[, ] state = new int[m, n];

        // 初始化行
        int sum = 0;
        for (int i = 0; i < m; i++) {
            sum += grid[i][0];
            state[i, 0] = sum;
        }
        // 初始化列
        sum = 0;
        for (int j = 0; j < n; j++) {
            sum += grid[0][j];
            state[0, j] = sum;
        }
        // 状态转移表
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                state[i, j] = Math.Min (state[i - 1, j], state[i, j - 1]) + grid[i][j];
            }
        }
        return state[m - 1, n - 1];
    }

    // [91] 解码方法
    // 动态规划
    public int NumDecodings (string s) {
        if (s[0] == '0') {
            return 0;
        }
        if (s.Length <= 1) {
            return s.Length;
        }
        int[] nums = s.Select (p => (int) (p - '0')).ToArray ();

        int last = 1;
        int lastlast = 1;
        for (int i = 1; i < s.Length; i++) {
            int _1 = 0;
            int _2 = 0;

            int num = nums[i - 1] * 10 + nums[i]; //前两位数
            if (num == 0) //连续两个0，直接返回0
            {
                return 0;
            }

            if (nums[i] > 0) {
                _1 = last;
            }

            if (nums[i - 1] != 0 && num <= 26) //前两位数小于26
            {
                _2 = lastlast;
            }
            lastlast = last;
            last = _1 + _2;
        }
        return last;
    }

    // [221] 最大正方形
    public int MaximalSquare (char[][] matrix) {
        int m = matrix.Length;
        if (m == 0)
            return 0;
        int n = matrix[0].Length;
        int maxLength = Math.Min (m, n);
        bool[][] dp = new bool[m][];

        bool hasValid = false;

        for (int i = 0; i < m; i++) {
            dp[i] = new bool[n];
            for (int j = 0; j < n; j++) {
                dp[i][j] = matrix[i][j] == '1';
                if (dp[i][j])
                    hasValid = true;
            }
        }
        if (!hasValid)
            return 0;

        int res = 1;
        for (int l = 2; l < maxLength; l++) {
            hasValid = false;
            for (int i = 0; i <= m - l; i++) {
                for (int j = 0; j <= n - l; j++) {
                    dp[i][j] = dp[i][j] && dp[i + 1][j + 1] && dp[i + 1][j] && dp[i][j + 1];
                    if (dp[i][j])
                        hasValid = true;
                }
            }

            if (hasValid)
                res++;
            else
                return res * res;
        }

        return res * res;
    }

    // [621] 任务调度器
    public int LeastInterval (char[] tasks, int n) {
        // 第一步 统计每种任务的数量
        int[] types = new int[26];
        foreach (char item in tasks) {
            types[item - 'A'] = types[item - 'A'] + 1;
        }

        // 第二步 对任务数量进行排序
        Array.Sort (types);

        //第三步 根据任务量最多（如A任务）的任务计算时间
        int max = types[25];
        int time = (max - 1) * (n + 1) + 1; // 最多任务为max，那么间隔有max-1个，间隔时间加上本身任务的运行时间
        int i = 24;

        //第四步 检查是否还有和任务最多数量一样多的任务，统计最后一个A运行完之后是否还有任务，这取决于和A数量一样多的任务
        while (i >= 0 && types[i] == max) {
            time++;
            i--;
        }

        return Math.Max (time, tasks.Length);
    }

    // [647] 回文子串
    int num = 0;
    public int CountSubstrings (string s) {
        for (int i = 0; i < s.Length; i++) {
            count (s, i, i); //回文串长度为奇数
            count (s, i, i + 1); //回文串长度为偶数
        }
        return num;
    }
    public void count (String s, int start, int end) {
        while (start >= 0 && end < s.Length && s[start] == s[end]) {
            num++;
            start--;
            end++;
        }
    }

    // [32] 最长有效括号
    public int LongestValidParentheses (string s) {
        int maxlen = 0;
        int strlen = s.Length;
        int[] p = new int[strlen];

        for (int i = 0; i < strlen; i++) {
            if (s[i] == ')') {
                if ((i - 1) >= 0 && s[i - 1] == '(') {
                    p[i] = 2;
                    if ((i - 2) >= 0) {
                        p[i] += p[i - 2];
                    }
                } else if ((i - 1) >= 0 && p[i - 1] > 0) {
                    if ((i - p[i - 1] - 1) >= 0 && s[(i - p[i - 1] - 1)] == '(') {
                        p[i] = p[i - 1] + 2;
                        if ((i - p[i - 1] - 2) >= 0) {
                            p[i] += p[i - p[i - 1] - 2];
                        }
                    }

                }
            }
            maxlen = Math.Max (p[i], maxlen);
        }

        return maxlen;
    }

    // [72] 编辑距离
    public int MinDistance (string word1, string word2) {
        int n1 = word1.Length;
        int n2 = word2.Length;

        int[][] dp = new int[n1 + 1][];
        for (int i = 0; i < n1 + 1; i++) {
            dp[i] = new int[n2 + 1];
        }

        for (int j = 1; j <= n2; j++) {
            dp[0][j] = dp[0][j - 1] + 1;
        }

        for (int i = 1; i <= n1; i++) {
            dp[i][0] = dp[i - 1][0] + 1;
        }

        for (int i = 1; i <= n1; i++) {
            for (int j = 1; j <= n2; j++) {
                if (word1[i - 1] == word2[j - 1]) dp[i][j] = dp[i - 1][j - 1];
                else dp[i][j] = Math.Min (Math.Min (dp[i - 1][j - 1], dp[i][j - 1]), dp[i - 1][j]) + 1;
            }
        }

        return dp[n1][n2];
    }
}