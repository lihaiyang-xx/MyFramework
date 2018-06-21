using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text;

public class Test {

	[Test]
	public void TestSimplePasses() {
        Debug.Log(FileTool.GetEncoding(Application.streamingAssetsPath + "/text.txt"));
		// Use the Assert class to test conditions.
	    CoroutineTool.DoCoroutine(FileTool.AsyncReadAllText(Application.streamingAssetsPath + "/text.txt", Encoding.UTF8,
	        Debug.Log));
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
