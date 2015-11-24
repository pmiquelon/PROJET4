using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.Diagnostics;
using System.Threading;


//TESTTTTTTTT

namespace Test
{

	public class Trigo : ILeapEventDelegate
	{
		// Intern
		private Controller controller = new Controller();
		private LeapEventListener listener;
		private Boolean isClosing = false;

		public Trigo ()
		{ }


//		public double GetAnglePIP(Bone proximal, Bone intermediate)
//		{
//
//			proximal.PrevJoint.x;
//			intermediate.NextJoint.x;
//
//			proximal.PrevJoint.y;
//			intermediate.NextJoint.y;
//
//			proximal.PrevJoint.z;
//			intermediate.NextJoint.z;
//
//
//			return;
//		}


//		
		public void Calculate()
		{
			Console.WriteLine ("----------------------------------------------------------------");
			Stopwatch start = new Stopwatch ();
			start.Start ();

			while (start.Elapsed.Seconds <= 10) 
			{
				foreach (Hand hand in controller.Frame().Hands) 
				{
					if (hand.IsLeft)
					{
						Console.WriteLine ("Nouvelle main gauche");

						foreach (Finger finger in hand.Fingers) 
						{
							Console.WriteLine ("Nouveau doigt");

							Bone bone; 
							foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
							{

							bone = finger.Bone (boneType);

//								if (finger.Type != Finger.FingerType.TYPE_THUMB) 
//								{
//									
//								}
							
							

			
								Console.WriteLine ("Allo3");
								Console.WriteLine (bone.PrevJoint.x +
								bone.NextJoint.x);
							}
						}
						Thread.Sleep (1000);

					
					}
				}
			}

		}




//		{
//			
//		this.controller = new Controller();
//		this.listener = new LeapEventListener(this);
//		controller.AddListener(listener);

//			Stopwatch start = new Stopwatch();
//			start.Start();
//			while (start.Elapsed.Seconds <= 10)
//			{
//				Console.WriteLine("----------------------------------------------------------------");
//				foreach (Hand hand in controller.Frame().Hands)
//				{
//					if (hand.IsValid) 
//					{
//
//						if (hand.IsLeft) 
//						{
//
//							foreach (Finger finger in hand.Fingers)
//							{
//
//								if (finger.IsValid)
//								{
//									if (finger.Type != Finger.FingerType.TYPE_THUMB) 
//									{
//										Bone bone = finger.Bone (Bone.BoneType.TYPE_METACARPAL);
//										Bone bone1 = finger.Bone (Bone.BoneType.TYPE_PROXIMAL);
//
//										if (bone.IsValid && bone1.IsValid)
//											Console.WriteLine (String.Format ("Angle proximal {0}: {1:0.00}",
//												finger.Type.ToString (), GetAngle (finger.Bone (Bone.BoneType.TYPE_METACARPAL), finger.Bone (Bone.BoneType.TYPE_PROXIMAL))));
//									}
//
//									Bone bone2 = finger.Bone (Bone.BoneType.TYPE_PROXIMAL);
//									Bone bone3 = finger.Bone (Bone.BoneType.TYPE_INTERMEDIATE);
//
//									if (bone2.IsValid && bone3.IsValid)
//										Console.WriteLine (String.Format ("Angle intermediate {0}: {1:0.00}",
//											finger.Type.ToString (), GetAngle (finger.Bone (Bone.BoneType.TYPE_PROXIMAL), finger.Bone (Bone.BoneType.TYPE_INTERMEDIATE))));
//
//									Bone bone4 = finger.Bone (Bone.BoneType.TYPE_INTERMEDIATE);
//									Bone bone5 = finger.Bone (Bone.BoneType.TYPE_DISTAL);
//
//									if (bone4.IsValid && bone5.IsValid)
//										Console.WriteLine (String.Format ("Angle distal {0}: {1:0.00} \n",
//											finger.Type.ToString (), GetAngle (finger.Bone (Bone.BoneType.TYPE_INTERMEDIATE), finger.Bone (Bone.BoneType.TYPE_DISTAL))));
//								} 
//
//								else
//									Console.WriteLine ("Certains doigts ne sont pas détecté");
//
//							}
//							Thread.Sleep (1000);
//						} 
//
//						else
//							Console.WriteLine ("On détecte la main droite");
//
//
//
//					}
//
//					else
//						Console.WriteLine ("La main n'est pas détectée");
//				}
//			}



		delegate void LeapEventDelegate(string EventName);
		public void LeapEventNotification(string EventName)
		{
			switch (EventName)
			{
			case "onInit":
				Debug.WriteLine("Init");
				break;
			case "onConnect":
				this.connectHandler();
				break;
			case "onFrame":
				if (!this.isClosing)
					this.newFrameHandler(this.controller.Frame());
				break;
			}
		}

		void connectHandler()
		{
			this.controller.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
			this.controller.Config.SetFloat("Gesture.Swipe.MinLength", 100.0f);
		}

		void newFrameHandler(Leap.Frame frame)
		{
			/*Console.WriteLine(String.Format("Frame id: {0}", frame.Id.ToString()));
        Console.WriteLine(String.Format("Frame id: {0}", frame.Timestamp.ToString()));
        Console.WriteLine(String.Format("Frame id: {0}", frame.CurrentFramesPerSecond.ToString()));
        Console.WriteLine(String.Format("Frame id: {0}", frame.IsValid.ToString()));
        Console.WriteLine(String.Format("Frame id: {0}", frame.Gestures().Count.ToString()));
        Console.WriteLine(String.Format("Frame id: {0}", frame.Images.Count.ToString()));*/
			Thread.Sleep(1000);
		}
	}

	public interface ILeapEventDelegate
	{
		void LeapEventNotification(string EventName);
	}

	public class LeapEventListener : Listener
	{
		ILeapEventDelegate eventDelegate;

		public LeapEventListener(ILeapEventDelegate delegateObject)
		{
			this.eventDelegate = delegateObject;
		}
		public override void OnInit(Controller controller)
		{
			this.eventDelegate.LeapEventNotification("onInit");
		}
		public override void OnConnect(Controller controller)
		{
			controller.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
			controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
			this.eventDelegate.LeapEventNotification("onConnect");
		}

		public override void OnFrame(Controller controller)
		{
			this.eventDelegate.LeapEventNotification("onFrame");
		}
		public override void OnExit(Controller controller)
		{
			this.eventDelegate.LeapEventNotification("onExit");
		}
		public override void OnDisconnect(Controller controller)
		{
			this.eventDelegate.LeapEventNotification("onDisconnect");
		}

	}

}
