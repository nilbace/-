// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("oLvhE6Rohn7epzamTiMSfVRkyV4c1Nx9Nbvkj6vFHNYpWz8mJkqyoLZJLwWcj43nRxH8r/x5YUjWDv2fEeimTiNJ81j0owgGlC1lkC4S0twjb+GvjvdUndtkcwNEcG586XiKXQLkuDlW+jqu+sEIw6bhRvQPeL4OIpATMCIfFBs4lFqU5R8TExMXEhFSaOQ28aKFTq1Q7Kx7ZyXQbpV5qt1SqTPlD8Lilkgack1soejCXZWbkBMdEiKQExgQkBMTErfRPEdf95WMRs9l5pghz2WE1xldvOReIET6HyRWegLwqH0o2D1PfnMT4JzQf3MnzDY+lD5PPyedNxzY+ydT6G2tfI2IkCy/fFAPutoExCxIBiVAnBXJWTTuK0+EznER9xARExIT");
        private static int[] order = new int[] { 4,5,13,10,13,6,13,7,8,13,12,12,13,13,14 };
        private static int key = 18;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
