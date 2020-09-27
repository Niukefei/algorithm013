// C#
public class Solution {

    // 387. 字符串中的第一个唯一字符
    public int FirstUniqChar (string s) {
        Dictionary<char, int> dic = new Dictionary<char, int> ();
        foreach (char c in s) {
            if (!dic.ContainsKey (c)) {
                dic[c] = 0;
            }
            dic[c]++;
        }
        for (int i = 0; i < s.Length; i++) {
            if (dic[s[i]] == 1) return i;
        }
        return -1;
    }

    // 541. 反转字符串 II
    public string ReverseStr (string s, int k) {
        char[] arr = s.ToCharArray ();
        for (int start = 0; start < arr.Length; start += 2 * k) {
            int i = start, j = Math.Min (start + k - 1, arr.Length - 1);
            while (i < j) {
                char tmp = arr[i];
                arr[i++] = arr[j];
                arr[j--] = tmp;
            }
        }
        return new String (arr);
    }

    // 151. 翻转字符串里的单词
    public string ReverseWords (string s) {
        s.Trim ();
        string[] words = s.Split (' ');
        Array.Reverse (words);
        return String.Join (" ", words).Trim ();
    }

    // 557. 反转字符串中的单词 III
    public string ReverseWords2 (string s) {
        int len = s.Length;
        char[] arr = s.ToCharArray ();
        int k = 0;
        while (k < len) {
            int start = k;
            while (k < len && arr[k] != ' ') {
                k++;
            }

            int i = start, j = k - 1;
            while (i < j) {
                char temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
            }
            while (k < len && arr[k] == ' ') {
                k++;
            }
        }
        return new string (arr);
    }

    // 917. 仅仅反转字母
    public string ReverseOnlyLetters (string S) {
        Stack<char> cc = new Stack<char> ();
        char[] c = S.ToCharArray ();
        for (int i = 0; i < S.Length; i++) {
            if (char.IsLetter (S[i])) {
                cc.Push (S[i]);
            }
        }
        for (var i = 0; i < c.Length; i++) {
            if (char.IsLetter (c[i]))
                c[i] = cc.Pop ();
        }
        return new string (c);
    }

    // 205. 同构字符串
    public bool IsIsomorphic (string s, string t) {
        string str = "";
        string str1 = "";
        for (int i = 0; i < s.Length; i++) {
            str += s.IndexOf (s[i]);
            str1 += t.IndexOf (t[i]);
        }
        return str == str1;
    }

    // 680. 验证回文字符串 Ⅱ
    public bool ValidPalindrome (string s) {
        return IsPalindrome (s, 0, s.Length - 1, false);
    }

    public bool IsPalindrome (string str, int p1, int p2, bool omited) {
        if (p1 >= p2) return true;
        if (str[p1] == str[p2]) return IsPalindrome (str, p1 + 1, p2 - 1, omited);
        else if (str[p1] != str[p2] && omited == false)
            return IsPalindrome (str, p1 + 1, p2, true) || IsPalindrome (str, p1, p2 - 1, true);
        else return false;
    }

    // 300. 最长上升子序列
    public int LengthOfLIS (int[] nums) {
        int len = nums.Length;
        if (len == 0) return 0;

        int[] dp = new int[len];
        dp[0] = 1;
        int max = 1;
        for (int i = 1; i < len; i++) {
            int maxTemp = 0;
            for (int j = 0; j < i; j++) {
                if (nums[i] > nums[j]) {
                    maxTemp = Math.Max (maxTemp, dp[j]);
                }
                dp[i] = maxTemp + 1;
                max = Math.Max (max, dp[i]);
            }
        }
        return max;
    }

    // 91. 解码方法
    public int NumDecodings (string s) {

        if (s[0] == '0') {
            return 0;
        }
        int[] sa = new int[s.Length + 1];
        sa[0] = 1;
        sa[1] = 1;
        for (int i = 1; i < s.Length; i++) {
            //后面一个位置为0时，和前一个相同
            if (i + 1 < s.Length && s[i + 1] == '0') {
                sa[i + 1] = sa[i];
                continue;
            }
            //当前位置为0时
            if (s[i] == '0') {
                //0前面如果是1/2 ,则和前一个相同；否则，编码有误返回0
                if (s[i - 1] == '1' || s[i - 1] == '2') {
                    sa[i + 1] = sa[i];
                    continue;
                } else {
                    return 0;
                }
            }
            //前一个位置为0时，计算到当前位置时，解码方法数不变，和前一个形同
            if (s[i - 1] == '0') {
                sa[i + 1] = sa[i];
                continue;
            }
            //普通不含0 的情况下，当前位置及前一位，构成的 2位数如果 小于等于26 ，则 解码数 是前两个解码数之和；否则和前一个相同
            int cur = (s[i - 1] - '0') * 10 + (s[i] - '0');
            if (cur <= 26) {
                sa[i + 1] = sa[i] + sa[i - 1];
            } else {
                sa[i + 1] = sa[i];
            }
        }
        return sa[s.Length];
    }

    // 8. 字符串转换整数 (atoi)
    public int MyAtoi (string str) {
        // 先去除两端空格
        string trimedStr = str.Trim ();
        // 通过StringBuilder来保存string答案。
        StringBuilder output = new StringBuilder ();
        // 为避免越界，先确认存在首字母。
        if (trimedStr.Length == 0) return 0;
        // 当首字符合规时
        if (trimedStr[0] == '+' || trimedStr[0] == '-' || (trimedStr[0] >= '0' && trimedStr[0] <= '9')) {
            output.Append (trimedStr[0]);
            for (int i = 1; i < trimedStr.Length; i++) {
                // 记录首个符号后的所有数字，遇到不是数字则break。
                if (trimedStr[i] >= '0' && trimedStr[i] <= '9') output.Append (trimedStr[i]);
                else break;
            }
        }
        // 当首字符不合规时
        else return 0;

        // 将output转化为int，并判断是否溢出/转换类型错误
        int answer = 0;
        checked {
            try {
                answer += Int32.Parse (output.ToString ());
            }
            // 当溢出时
            catch (OverflowException oEx) {
                if (trimedStr[0] == '-') return Int32.MinValue;
                else return Int32.MaxValue;
            }
            // 当output格式有误、即无法转换时（如为"+-"）
            catch (FormatException e) {
                return 0;
            }
        }
        // 转换成功
        return answer;
    }

    // 438. 找到字符串中所有字母异位词
    public IList<int> FindAnagrams (string s, string p) {
        IList<int> res = new List<int> ();
        Dictionary<char, int> need = new Dictionary<char, int> ();
        Dictionary<char, int> window = new Dictionary<char, int> ();
        foreach (var item in p) {
            if (!need.ContainsKey (item)) {
                need.Add (item, 1);
            } else {
                need[item]++;
            }
        }
        int left = 0;
        int right = 0;
        int vailde = 0;
        while (right < s.Length) {
            char c = s[right];
            right++;
            if (need.ContainsKey (c)) {
                if (!window.ContainsKey (c)) {
                    window.Add (c, 1);
                } else {
                    window[c]++;
                }
                if (window[c] == need[c]) {
                    vailde++;
                }
            }
            while (vailde == need.Count) {
                if (right - left == p.Length) {
                    res.Add (left);
                }
                char d = s[left];
                left++;
                if (need.ContainsKey (d)) {
                    if (window[d] == need[d]) {
                        vailde--;
                    }
                    window[d]--;
                }
            }
        }
        return res;
    }

    // 5. 最长回文子串
    public string LongestPalindrome (string s) {
        string result = "";
        int n = s.Length;
        int end = 2 * n - 1;
        for (int i = 0; i < end; i++) {
            double mid = i / 2.0;
            int p = (int) (Math.Floor (mid));
            int q = (int) (Math.Ceiling (mid));
            while (p >= 0 && q < n) {
                if (s[p] != s[q]) break;
                p--;
                q++;
            }
            int len = q - p - 1;
            if (len > result.Length)
                result = s.Substring (p + 1, len);
        }
        return result;
    }

    // 32. 最长有效括号
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

    // 44. 通配符匹配
    public bool IsMatch (string s, string p) {
        int i = 0; //指向字符串s
        int j = 0; //指向字符串p
        int startPos = -1; //记录星号的位置
        int match = -1; //用于匹配星号
        while (i < s.Length) {
            //表示相同或者p中为?
            if (j < p.Length && (s[i] == p[j] || p[j] == '?')) {
                i++;
                j++;
            }
            //匹配到了星号，记录下的位置
            else if (j < p.Length && p[j] == '*') {
                startPos = j;
                match = i;
                j = startPos + 1;
            }
            //以上都没有匹配到，回到星号所在的位置，往后再次匹配
            else if (startPos != -1) {
                match++;
                i = match;
                j = startPos + 1;
            } else {
                return false;
            }
        }
        //去除多余的星号
        while (j < p.Length && p[j] == '*') j++;
        return j == p.Length;
    }
}