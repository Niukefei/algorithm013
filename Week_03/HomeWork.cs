// C#
public class Solution {

    // [236] 二叉树的最近公共祖先
    public TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
        // 终止条件：
        // 1.当越过叶子节点，直接返回null
        // 2.当root等于p或q，直接返回root
        if (root == null || root == p || root == q) return root;
        // drill down
        // 1.开启递归左子节点，返回值记为left
        // 2.开启递归右子节点，返回值记为right
        TreeNode left = LowestCommonAncestor (root.left, p, q);
        TreeNode right = LowestCommonAncestor (root.right, p, q);
        // 根据返回值left和right，产生四种情况：
        // 1.left和right同时为空：说明root的左右子树都不包含p，q，返回null
        // 2.当left和right同时不为null：说明p，q分别在root的异侧，因此root为最近公共祖先，返回root
        // 3.当left为空，right不为空：p，q都不在root的座左子树中，直接返回right。具体可分为两种情况：
        //      (1)p，q其中一个在root的右子树中，此时right指向p；
        //      (2)p，q两节点都在root的右子树中，此时right指向最近公共祖先节点；
        // 4.当left不为空，right为空：与情况3同理；
        if (left == null && right == null) return null; // 1.
        if (left == null) return right; // 3.
        if (right == null) return left; // 4.
        return root; // 2.if(left != null && right != null)
    }

    // [105] 从前序与中序遍历序列构造二叉树
    public TreeNode BuildTree (int[] preorder, int[] inorder) {
        return buildTreeHelper (preorder, 0, preorder.Length, inorder, 0, inorder.Length);
    }
    private TreeNode buildTreeHelper (int[] preorder, int p_start, int p_end, int[] inorder, int i_start, int i_end) {
        // preorder 为空，直接返回 null
        if (p_start == p_end) {
            return null;
        }
        int root_val = preorder[p_start]; // 根节点的值
        // 构建当前的根节点
        TreeNode root = new TreeNode (root_val);
        //在中序遍历中找到根节点的位置
        int i_root_index = 0;
        for (int i = i_start; i < i_end; i++) {
            if (root_val == inorder[i]) {
                i_root_index = i;
                break;
            }
        }
        int leftNum = i_root_index - i_start;
        //递归的构造左子树
        root.left = buildTreeHelper (preorder, p_start + 1, p_start + leftNum + 1, inorder, i_start, i_root_index);
        //递归的构造右子树
        root.right = buildTreeHelper (preorder, p_start + leftNum + 1, p_end, inorder, i_root_index + 1, i_end);
        return root;
    }

    // [77] 组合
    public IList<IList<int>> Combine (int n, int k) {
        List<IList<int>> ans = new List<IList<int>> ();
        if (n <= 0 || k <= 0 || n < k) return ans;
        DFS (n, k, 1, new List<int> (), ans);
        return ans;
    }
    public void DFS (int n, int k, int begin, List<int> list, List<IList<int>> ans) {
        if (list.Count == k) {
            ans.Add (new List<int> (list)); // 数够了，添加到结果里
            return;
        }
        // 此处注意i的上界(此处等同于剪枝处理)
        for (int i = begin; i <= n; i++) {
            list.Add (i);
            DFS (n, k, i + 1, list, ans);
            list.RemoveAt (list.Count - 1);
        }
    }

    // [46] 全排列
    public IList<IList<int>> Permute (int[] nums) {
        List<IList<int>> ans = new List<IList<int>> ();
        if (nums.Length == 0) return ans;
        bool[] used = new bool[nums.Length];
        dfs (0, nums, used, new List<int> (), ans);
        return ans;
    }
    public void dfs (int idx, int[] nums, bool[] used, List<int> list, List<IList<int>> ans) {
        if (idx == nums.Length) {
            ans.Add (new List<int> (list)); // 数够了，添加到结果里
            return;
        }
        for (int i = 0; i < nums.Length; i++) {
            if (!used[i]) {
                // 添加并标记为已使用
                list.Add (nums[i]);
                used[i] = true;
                dfs (idx + 1, nums, used, list, ans); // 注意idx更新，已占用一个格子
                // 重置状态
                used[i] = false;
                list.RemoveAt (list.Count - 1);
            }
        }
    }

    // [47] 全排列II
    public IList<IList<int>> PermuteUnique (int[] nums) {
        List<IList<int>> ans = new List<IList<int>> ();
        if (nums.Length == 0) return ans;
        Array.Sort (nums); // 排序（升序或者降序都可以），排序是剪枝的前提
        bool[] used = new bool[nums.Length];
        dfs (0, nums, used, new List<int> (), ans);
        return ans;
    }
    public void dfs (int idx, int[] nums, bool[] used, List<int> list, List<IList<int>> ans) {
        if (idx == nums.Length) {
            ans.Add (new List<int> (list)); // 数够了，添加到结果里
            return;
        }
        for (int i = 0; i < nums.Length; i++) {
            if (used[i]) continue;
            // 剪枝条件：i > 0 是为了保证 nums[i - 1] 有意义
            // 写 !used[i - 1] 是因为 nums[i - 1] 在深度优先遍历的过程中刚刚被撤销选择
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) continue;

            // 添加并标记为已使用
            list.Add (nums[i]);
            used[i] = true;
            dfs (idx + 1, nums, used, list, ans); // 注意idx更新，已占用一个格子
            // 重置状态
            used[i] = false;
            list.RemoveAt (list.Count - 1);
        }
    }
}