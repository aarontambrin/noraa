using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galaxy2 : MonoBehaviour {


		ParticleSystem particlesystem;

	float randomvalue;
	public ParticleSystem thiscloudparticles;
	public ParticleSystem thiscloudparticles2;
	public GameObject colliderparent;
	GameObject newstarcollider;
	public int scale = 1;
	public GameObject starcollider;
	ParticleSystem.Particle[] cloudpoints;
	ParticleSystem.Particle[] points;
	ParticleSystem.Particle[] cloudpoints2;
	[HideInInspector]
	public int totalspiralparticles=0;
	[HideInInspector]
	public int totalcloudparticles=0;
	public int numberofstars;
	public int numberofspiralparticles;
	public int numberofcloudparticles;
	float radiusAbase=0.5f;
	float radiusBbase=0.625f;
	float radiusA;
	float radiusB;
	float ellipseparentangle= 0;
	public int randomseed;
	public float ellipseoffsetangle;
	public float iterations;
	[HideInInspector]
	public int starcount;
	float ellipseratio=0.8f;
	float radiusAincrement;
	int iterationb=1;
	Vector3 newstarvector;
	int arrayposition=0;
	int cloudarrayposition=0;
	int cloudarrayposition2=0;
	float angle;
	float starrandomnumber;
	int starornotmod=6;
	bool starornot;
	float x=0;
	float y=0;
	float z=0;
	int randomy;
	int starornotmodulator=0;
	bool yplusorminus=true;
	float starsize,cloudsize, cloudsize2, cloudrotation;
	public float conststarsize;
	Color cloudcolour,starcolor;
	float maxradius=0;
	float spiralcloudrangea,spiralcloudrangeb,spiralcloudrangec,spiralcloudranged;
	int redgiant=0;
	int bluegiant=0;
	int deepbluegiant=0;
	int palebluegiant=0;
	int whitestar=0;
	int yellowstar=0;
	int orangestar=0;
	int redstar=0;
	int whitedward=0;
	int reddwarf=0;
	float maxx=0;
	float maxy=0;
	float maxz=0;
	float xmod,ymod,zmod;
	int yloop2=0;
	int clouds2count=0;


		
		void Start () {
		ellipseoffsetangle = (ellipseoffsetangle * (100 / iterations))*8;
		float constiterations = iterations;
		float totalradius = radiusBbase + (scale * iterations);
		float ellipseratioadjust = 0.2f / (constiterations*0.3f);
		float constellipseoffsetangle = ellipseoffsetangle;
		float ycycle = (constiterations*0.3f);
		int halfycycle = ((int)ycycle / 2);
		particlesystem = GetComponent<ParticleSystem> ();
		//thiscloudparticles = GetComponent<ParticleSystem> ();
		//thiscloudparticles2 = GetComponent<ParticleSystem> ();
		points = new ParticleSystem.Particle[numberofstars];
		cloudpoints = new ParticleSystem.Particle[numberofspiralparticles];
		cloudpoints2 = new ParticleSystem.Particle[numberofcloudparticles];	
			for (int yloop = 0; yloop < ycycle; yloop++) {
				radiusA = radiusAbase;
				radiusB = radiusBbase;
				Random.InitState (randomseed);

				for (int i = 1; i < iterations; i++) {
				
					if (arrayposition < numberofstars) {
						plotellipse (radiusA, radiusB);
					}

					starornotmodulator = starornotmodulator + 1;
					if (starornotmodulator > 11) {
						starornotmod++;
						starornotmodulator = 0;
					}

				radiusB = radiusB + scale;
				radiusA= radiusB * ellipseratio;

				if (iterationb > (constiterations*0.7f)) { 
				ellipseratio = ellipseratio + ellipseratioadjust;	
					}

				iterationb++;

				}
				if (yloop2 == halfycycle) {
					yplusorminus = !yplusorminus;
					y = 0;
					iterations = constiterations;
				}
				if (yplusorminus == true) {
				y=y+scale;
				}
				if (yplusorminus == false) {
				y=y-scale;
				}
				ellipseparentangle = 0;
				ellipseratio = 0.8f;
				ellipseoffsetangle = constellipseoffsetangle;
				iterationb=1;
				starornotmod = 6+(int)(Mathf.Abs(y)/6);
				iterations = iterations - (constiterations / 40);
				randomseed = randomseed + 12;
				if (radiusB > maxradius) {
					maxradius = radiusB;
				}
				yloop2++;
			}


		Debug.Log ("Total Size= " + " Width/X: " + (maxx * 2) + " Depth/Z: " + (maxz * 2) + " Height/Y: " + (maxy * 2));
		particlesystem.SetParticles (points, points.Length);
		thiscloudparticles.SetParticles (cloudpoints,cloudpoints.Length);
		thiscloudparticles2.SetParticles (cloudpoints2,cloudpoints2.Length);
		Debug.Log (redstar + "  " + orangestar + "  " + yellowstar + "  " + whitestar + "  " + whitedward + "  " + reddwarf + "  " + palebluegiant + "  " + deepbluegiant + "  " + bluegiant + "  " + redgiant);
		Debug.Log ("total star particles: " + starcount + " Total spiral cloud particles: " + totalspiralparticles + " Total Disc cloud particles: " + totalcloudparticles++);
		}

		public float ellipseperimeter(float radiusA, float radiusB){
			float perimeter=  Mathf.PI *(3 *(radiusA + radiusB) -Mathf.Sqrt(((3 * radiusA) + radiusB) * (radiusA + (3 * radiusB))));
			return perimeter;
		}
		public void plotellipse(float radiusA, float radiusB){
		float perimeter = ellipseperimeter (radiusA, radiusB);
		float segments  = Mathf.Round (perimeter/scale);

		for (int i = 0; i < segments; i++) {

				x = Mathf.Sin (Mathf.Deg2Rad * angle) * radiusA;
				z = Mathf.Cos (Mathf.Deg2Rad * angle) * radiusB;
				if (arrayposition < numberofstars) {
					randomgenerator ();
					starornotfunction (starrandomnumber);
					if (starornot == true) {
						 xmod= xmodifier ();
						 ymod=	ymodifier ();
						 zmod= zmodifier ();
						
						Vector3 starvector = new Vector3 (x+xmod, y+ymod, z+zmod);
						maxsize ();
						newstarvector = ellipserotation (starvector, ellipseparentangle);
						spiralcloudrange ();
						stardata (starrandomnumber);
						
						points [arrayposition].position = newstarvector;
						points [arrayposition].startColor = starcolor;
						points [arrayposition].startSize = conststarsize*starsize*scale;


						
					if (((angle > spiralcloudrangea) && (angle < spiralcloudrangeb)) || ((angle > spiralcloudrangec) && (angle < spiralcloudranged))) {
						getcloudcolour (starrandomnumber);
						cloudpoints [cloudarrayposition].position = newstarvector;
						cloudpoints [cloudarrayposition].startSize = cloudsize * scale;
						cloudpoints [cloudarrayposition].startColor = cloudcolour;
						cloudarrayposition++;
						totalspiralparticles++;
							
					} 
						
						

					newstarcollider= Instantiate (starcollider, newstarvector, Quaternion.identity);
					newstarcollider.transform.parent = colliderparent.transform;
					initiatestar thisinitiatestar = newstarcollider.GetComponent<initiatestar>();
					thisinitiatestar.thisstarnumber = starcount;
					thisinitiatestar.thisstarrandomnumber = starrandomnumber;
					SphereCollider spherecollider = newstarcollider.GetComponent<SphereCollider> ();
					spherecollider.radius = ((conststarsize * starsize*scale) /3f);
					arrayposition++;
					starcount++;
					}
				if (clouds2count > (100+Remap(iterationb,0,iterations,0,200))) {
					getcloudcolour2 (starrandomnumber);
					cloudpoints2 [cloudarrayposition2].position = newstarvector;
					cloudpoints2 [cloudarrayposition2].startSize = cloudsize2;
					cloudpoints2 [cloudarrayposition2].startColor = cloudcolour;
					cloudarrayposition2++;
					totalcloudparticles++;
					clouds2count = 0;

				}
				clouds2count++;
				}
				angle += (360f / segments);
				if (angle > 360) {
					angle = 0;
				}
			}
			ellipseparentangle = ellipseparentangle + ellipseoffsetangle;
			if (ellipseparentangle >= 360) {
			ellipseparentangle = 0;
			}
		}
		public  Vector3 ellipserotation(Vector3 starvector, float ellipseparentangle){
			newstarvector = Quaternion.Euler (0, ellipseparentangle, 0) * starvector;
			return newstarvector;
		}
		public void randomgenerator (){
			 randomvalue = UnityEngine.Random.value;
			starrandomnumber  = (float)(System.Math.Round(randomvalue,6)*1000000);
		}
		public void starornotfunction(float starrandomnumber){
			int starremainder = (int)starrandomnumber % starornotmod;
			if (starremainder==3){
				starornot = true;
			}
			else  starornot = false;

		}
		public float xmodifier(){
		randomgenerator ();
		float	thisxmod =((float)System.Math.Round(randomvalue,2)*2);


		int xmodplusorminus = (int)starrandomnumber % 2;
			if (xmodplusorminus == 1) {
				thisxmod = -thisxmod;

			}
			return thisxmod;
		}

	public float ymodifier(){
		randomgenerator();
		float thisymod = ((float)System.Math.Round(randomvalue,2)*2);

		int ymodplusorminus = (int)starrandomnumber % 2;
			if (ymodplusorminus == 1) {
				thisymod = -thisymod;

			}
			return thisymod;
		}
		public float zmodifier(){
		randomgenerator ();
		float	thiszmod = ((float)System.Math.Round(randomvalue,2)*2);
		float	zmodplusorminus = (int)starrandomnumber % 2;
			//Debug.Log (starrandomnumber+"  "+thiszmod+"  "+zmodplusorminus);

			if (zmodplusorminus == 1) {
				thiszmod = -thiszmod;

			}
			return thiszmod;
		}
		public Vector4 getcloudcolour (float starrandomnumber){
		float thiscloudsize = 0;
			float cloudvalue = starrandomnumber % 48;
			if (cloudvalue<13){
				cloudcolour = new Color (1f, 1f, 1f,0.2f);
			 thiscloudsize = 3;
			}
			if ((cloudvalue>12)&&(cloudvalue<25)){
				cloudcolour = new Color (0.87f, 0.95f, 1f,0.0f);
				thiscloudsize = 3;
			}
			if ((cloudvalue>24)&&(cloudvalue<46)){
				cloudcolour = new Color (0.81f,0.86f,0.96f,0.0f);
				thiscloudsize = 1.5f;
			}
			if (cloudvalue==47){
				cloudcolour = new Color (1f,0.12f,0f,0.3f);
				thiscloudsize = 2.5f;
			}
		if (cloudvalue==46){
			cloudcolour = new Color (0f,0f,0f,0.5f);
			thiscloudsize =3;
		}
		cloudsize = scale*thiscloudsize*(Remap(iterationb,1,iterations,1,10));
		return cloudcolour;
		}

		public Vector4 getcloudcolour2 (float starrandomnumber){
		float thiscloudsize=0;
			float cloudvalue = starrandomnumber % 48;
			if (cloudvalue<13){
				cloudcolour = new Color (1f, 1f, 1f,1f);
				thiscloudsize = 70;
			}
			if ((cloudvalue>12)&&(cloudvalue<25)){
				cloudcolour = new Color (0.77f, 0.87f, 1f,1f);
				thiscloudsize = 90;
			}
			if ((cloudvalue>24)&&(cloudvalue<36)){
				cloudcolour = new Color (0.8f,0.84f,0.9f,1f);
				thiscloudsize = 100f;
			}
		if (cloudvalue>35){
			cloudcolour = new Color (0.8f,0.84f,0.9f,1f);
			thiscloudsize = 90f;
		}
		 cloudrotation = (starrandomnumber % 360) * Mathf.Deg2Rad;
			cloudsize2 = scale*thiscloudsize;
			return cloudcolour;
		}
	public void spiralcloudrange(){
		if (radiusB < (iterations / 6)) {
			spiralcloudrangea = 1;
			spiralcloudrangeb = 180;
			spiralcloudrangec = 181;
			spiralcloudranged = 360;
		}
		if (radiusB > (iterations /6)) {
			spiralcloudrangea = 65;
			spiralcloudrangeb = 115;
			spiralcloudrangec = 245;
			spiralcloudranged = 295;
		}
	}
	public void stardata( float starrandomnumber ){
		int startypeconstant = ((int)starrandomnumber % 1000);
		if (((angle > spiralcloudrangea) && (angle < spiralcloudrangeb)) || ((angle > spiralcloudrangec) && (angle < spiralcloudranged))) {
			startypeconstant = startypeconstant + 50;

		}
		//blue giant star
		if  (startypeconstant > 995) {
			starsize =  4;
			starcolor = new Color (0.369f, 0.635f, 1, 1);
			bluegiant++;
			}
		//deep blue star
		else if ((startypeconstant > 980) && (startypeconstant < 996)) {
			starsize = 3.5f;
			starcolor = new Color (0.435f, 0.671f, 1, 1);
			deepbluegiant++;
		}
		//pale blue star
		else if ((startypeconstant > 960) && (startypeconstant < 981)) {
			starsize = 3;
			starcolor=new Color(0.890f,0.937f,1,1);
			palebluegiant++;
			} 
		//brown dwarf
		else if ((startypeconstant > 930) && (startypeconstant < 961)) {
			starsize =  0.3f;
			starcolor = new Color (0.96f,0.51f,0.16f,1);
			reddwarf++;
		}
		//white dwarf
		else if ((startypeconstant > 901) && (startypeconstant < 931)) {
			starsize =  0.4f;
			starcolor = new Color (1, 1,0.85f,1);
			whitedward++;
		}
		//red giant
		else if (startypeconstant == 901) {
				starsize=6;
				starcolor = new Color (1, 0.71f, 0.51f, 1);
				redgiant++;
			}
		//white star
		else if ((startypeconstant > 675) && (startypeconstant < 901)) {
			starsize =  1.5f;
			starcolor = new Color (1, 1, 1, 1);
			whitestar++;
		}
		//yellow white star
		else if ((startypeconstant > 450) && (startypeconstant < 676)) {
			starsize = 1;
			starcolor = new Color (1, 1, 0.64f, 1);
			yellowstar++;
		}
		//yellow orange star
		else if ((startypeconstant > 225) && (startypeconstant < 451)) {
			starsize = 0.7f;
			starcolor = new Color (1, 0.97f, 0.79f, 1);
			orangestar++;
		}
		//red orange star
		else if ((startypeconstant > 0) && (startypeconstant < 226)) {
			starsize = 0.6f;
			starcolor = new Color (1, 0.95f, 0.82f, 1);
			redstar++;
		}

	}

		public  float Remap ( float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	

	}
	public float inverseremap(float value, float from1, float to1, float from2, float to2){
		float numbertoinverse = Remap (value, from1, to1, from2, to2);
		float inversenumber = (to2 - numbertoinverse);
		return (inversenumber);
	}
	public void maxsize (){
		if (x  > maxx) {
			maxx = x;
		}
		if (y> maxy) {
			maxy = y;
			//Debug.Log (maxy);
		}
		if (z > maxz) {
			maxz =z;

		}

	}
	}

