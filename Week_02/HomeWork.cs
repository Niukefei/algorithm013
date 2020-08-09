// C#
public class Solution {
    // [242] 有效的字母异位词
    // 字典法 {char:count}
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

    // [49] 字母异位词分组
    // 哈希表法 {sortStr:list}
    public IList<IList<string>> GroupAnagrams (string[] strs) {
        Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>> ();
        foreach (string str in strs) {
            // Concat:将指定字符串连接到此字符串的结尾
            // OrderBy 将char升序排列
            // rt是排序过的字母 eat -> aet
            string key = String.Concat (str.OrderBy (ch => ch));
            if (dic.ContainsKey (key)) {
                dic[key].Add (str);
            } else {
                dic[key] = new List<string> { str };
            }
        }
        List<IList<string>> ans = new List<IList<string>> ();
        foreach (List<String> v in dic.Values) {
            ans.Add (v);
        }
        return ans;
    }

    // [1] 两数之和
    // 字典法
    public int[] TwoSum (int[] nums, int target) {
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        int sub;
        for (int i = 0; i < nums.Length; i++) {
            sub = target - nums[i];
            if (dic.ContainsKey (sub)) {
                return new int[] { dic[sub], i };
            }
            dic[nums[i]] = i;
        }
        return null;
    }

    // [94] 二叉树的中序遍历
    // 递归法
    public IList<int> InorderTraversal (TreeNode root) {
        IList<int> res = new List<int> ();
        if (root != null) helper (root, res);
        return res;
    }
    public void helper (TreeNode root, IList<int> res) {
        if (root.left != null) helper (root.left, res);
        res.Add (root.val);
        if (root.right != null) helper (root.right, res);
    }
}