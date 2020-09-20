// C#
public class Solution {

    // 191. 位1的个数
    public int HammingWeight (uint n) {
        int count = 0;
        //for (int i = 0; i < 32; i++)
        //{
        //    count += (int)(n & 1);
        //    n >>= 1;
        //}
        //return count;

        while (n > 0) {
            n &= (n - 1); // 打掉最后一位的1
            count++;
        }
        return count;
    }

    // 231. 2的幂
    public bool IsPowerOfTwo (int n) {
        if (n <= 0) return false;
        return (n &= (n - 1)) == 0; // 2的n次幂只能有一位是1
    }

    // 190. 颠倒二进制位
    public uint reverseBits (uint n) {
        uint r = 0;
        for (int i = 0; i < 32; i++) {
            var f = (uint) 1 << i;
            if ((n & f) == f) r ^= (uint) 1 << (31 - i);
        }
        return r;
    }

    // 1122. 数组的相对排序
    public int[] RelativeSortArray (int[] arr1, int[] arr2) {
        List<int> list = arr2.ToList ();
        arr1 = arr1.OrderBy (a => list.IndexOf (a)).OrderBy (a => list.IndexOf (a) == -1 ? a : 0).ToArray ();
        return arr1;
    }

    // 242. 有效的字母异位词
    public bool IsAnagram (string s, string t) {
        if (s.Length != t.Length) return false;
        int[] table = new int[26];
        for (int i = 0; i < s.Length; i++) {
            table[s[i] - 'a']++;
            table[t[i] - 'a']--;
        }
        foreach (int v in table) {
            if (v != 0) return false;
        }
        return true;
    }

    // 146. LRU缓存机制
    public class LRUCache {

        int capacity;
        Node head, tail;
        Dictionary<int, Node> LRUDic;
        public class Node {
            public int key;
            public int value;
            public Node pre;
            public Node next;
        }
        public LRUCache (int capacity) {
            this.capacity = capacity;
            LRUDic = new Dictionary<int, Node> ();
            head = new Node ();
            tail = new Node ();
            head.next = tail;
            tail.pre = head;

        }

        public int Get (int key) {
            if (LRUDic.ContainsKey (key)) {
                int value = LRUDic[key].value;
                DelletNode (LRUDic[key]);
                AddNodeAtHead (LRUDic[key]);
                return value;
            } else {
                return -1;
            }
        }

        public void Put (int key, int value) {
            if (LRUDic.ContainsKey (key)) {
                Node node = LRUDic[key];
                DelletNode (node);
                node.value = value;
                AddNodeAtHead (node);
                return;
            }
            if (capacity > LRUDic.Count) {
                Node node = new Node ();
                node.key = key;
                node.value = value;
                LRUDic.Add (key, node);
                AddNodeAtHead (node);
            } else {
                Node node = LRUDic[tail.pre.key];
                LRUDic.Remove (DelletTail ());
                node.key = key;
                node.value = value;
                AddNodeAtHead (node);
                LRUDic.Add (key, node);
            }
        }

        public void DelletNode (Node node) {
            node.pre.next = node.next;
            node.next.pre = node.pre;
        }

        public void AddNodeAtHead (Node node) {
            node.next = head.next;
            head.next.pre = node;
            head.next = node;
            node.pre = head;

        }

        public int DelletTail () {
            int key = tail.pre.key;
            Node node = tail.pre;
            node.pre.next = tail;
            tail.pre = node.pre;
            return key;
        }
    }

    // 56. 合并区间
    public static int[][] Merge (int[][] intervals) {
        Array.Sort (intervals, (a, b) => a[0] - b[0]);
        int[][] res = new int[intervals.Length][];
        int index = 0;
        for (int i = 0; i < intervals.Length; i++) {
            var lastIndex = index - 1;
            if (res[0] == null || res[lastIndex][1] < intervals[i][0]) {
                res[index] = intervals[i];
                index++;
            } else {
                res[lastIndex][1] = Math.Max (intervals[i][1], res[lastIndex][1]);
            }
        }

        return res.Where (a => a != null).ToArray ();
    }

    // 51. N 皇后
    // 对于所有的主对角线有 行号 + 列号 = 常数，对于所有的次对角线有 行号 - 列号 = 常数.
    // 回溯法
    List<IList<string>> ans = new List<IList<string>> ();
    int[] rows; // 用于标记是否被列方向的皇后攻击
    int[] na; // 用于标记是否被主对角线方向的皇后攻击
    int[] pie; // 用于标记是否被次对角线方向的皇后攻击
    int[] queens; // 用于存储皇后放置的位置
    int n;

    public IList<IList<string>> SolveNQueens (int n) {
        // 初始化
        rows = new int[n];
        na = new int[2 * n - 1];
        pie = new int[2 * n - 1];
        queens = new int[n];
        this.n = n;

        // 从第一行开始回溯求解 N 皇后
        backtrack (0);
        return ans;
    }
    // 在第一行放置皇后
    private void backtrack (int row) {
        if (row >= n) return;
        // 分别尝试在第row行的每一列放置皇后
        for (int col = 0; col < n; col++) {
            // 判断当前放置的皇后不被其他皇后的攻击
            if (isNotUnderAttack (row, col)) {
                // 选择，在当前的位置上放置皇后
                placeQueen (row, col);
                // 当当前行是最后一行，则找到了一个解决方案
                if (row == n - 1) addSolution ();
                // 在下一行中放置皇后
                backtrack (row + 1);
                // 撤销，回溯，即将当前位置的皇后去掉
                removeQueen (row, col);
            }
        }
    }
    // 判断 row 行，col 列这个位置有没有被其他方向的皇后攻击
    private bool isNotUnderAttack (int row, int col) {
        // 判断的逻辑是：
        //      1. 当前位置的这一列方向没有皇后攻击
        //      2. 当前位置的主对角线方向没有皇后攻击
        //      3. 当前位置的次对角线方向没有皇后攻击
        int res = rows[col] + na[row - col + n - 1] + pie[row + col];
        // 如果三个方向都没有攻击的话，则 res = 0，即当前位置不被任何的皇后攻击
        return res == 0;
    }
    // 在指定的位置上放置皇后
    private void placeQueen (int row, int col) {
        // 在 row 行，col 列 放置皇后
        queens[row] = col;
        // 当前位置的列方向已经有皇后了
        rows[col] = 1;
        // 当前位置的主对角线方向已经有皇后了
        na[row - col + n - 1] = 1;
        // 当前位置的次对角线方向已经有皇后了
        pie[row + col] = 1;
    }
    // 移除指定位置上的皇后
    private void removeQueen (int row, int col) {
        // 移除 row 行上的皇后
        queens[row] = 0;
        // 当前位置的列方向没有皇后了
        rows[col] = 0;
        // 当前位置的主对角线方向没有皇后了
        na[row - col + n - 1] = 0;
        // 当前位置的次对角线方向没有皇后了
        pie[row + col] = 0;
    }
    // 将满足条件的皇后位置放入ans中
    public void addSolution () {
        List<string> solution = new List<string> ();
        for (int i = 0; i < n; ++i) {
            int col = queens[i];
            string str = "";
            for (int j = 0; j < col; ++j) str += '.';
            str += 'Q';
            for (int j = 0; j < n - col - 1; ++j) str += '.';
            solution.Add (str);
        }
        ans.Add (solution);
    }
}