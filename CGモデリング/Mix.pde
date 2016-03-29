float rx = 0.0;  // x軸まわりの回転角度
float ry = 0.0;  // y軸まわりの回転角度
float rz = 0.0;  // z軸まわりの回転角度

String file_name="save-11.jpg"; 
float[][][] Sp;//球
float[][][] Py; // 角錐
float[][][] Cy;//角柱
float[][][] To;//トーラス

float[] Light= {
  -0.5, 1, 0.2
};
float[] col= {
  1, 1, 1
};

void setup() {
  size(800, 800, P3D);  // 3Dモード
  noFill();             // 塗り潰し（なしモード）
  noStroke();          // 輪郭線色（白）

  Sp= new float[1104][3][3];//球
  Py= new float[144][3][3];// 角錐
  Cy= new float[480][3][3];//角柱
  To= new float[400][3][3];//トーラス

  TrmSphere(Sp, 24, 0.8); // 球
  TrmPyramid(Py, 72, 1, 1);//角錐
  TrmCylinder(Cy, 120, 1, 1);//角柱
  TrmTorus(To, 20, 10, 1, 0.2); // トーラス

  float l= sqrt(Light[0]*Light[0]+Light[1]*Light[1]+Light[2]*Light[2]);
  Light[0]/=l; 
  Light[1]/=l; 
  Light[2]/=l;
}

void draw() {
  // 画面をリフレッシュ
  background(128);    // 背景（灰色）
  ortho(-width/2, width/2, -height/2, height/2, -10, 10);  // 並行投影  

  translate(width/2, height/2, 0); // 画面の中心に原点を移動
  rotateX(rx);   // x軸まわりの回転
  rotateY(ry);   // y軸まわりの回転
  rotateZ(rz);   // z軸まわりの回転
  scale(250);    // 250倍に拡大
  translate(0, 0, 0); //の中心に移動

  // 立体を描画
  // draw_triangle(Py, 160);  // サンプル

  //頭
  setcolor(1, 1, 1);
  scale(0.5, 0.5, 0.5);
  rotateX(PI/2); 
  rend_triangle(Sp, 1104);
  
  //身体
  scale(1.3, 1.3, 1.3);
  translate(0, 0, -1.2);
  rend_triangle(Sp, 1104);
  
  //目
  setcolor(0, 1, 1);
  scale(0.1, 0.1, 0.1);
  translate(2, 5, 13);
  rend_triangle(Sp, 1104);
  
  translate(-4, 0, 0);
  rend_triangle(Sp, 1104);
  
  //ボタン
  setcolor(1, 1, 0);
  translate(2, 2.5, -16);
  rend_triangle(Sp, 1104);
  translate(0, 0.5, 3);
  rend_triangle(Sp, 1104);
  translate(0, -0.5, 3);
  rend_triangle(Sp, 1104);
  
  //鼻
  setcolor(1, 0, 0);
  translate(0, -2, 9);
  rotateX(3*PI/2); 
  scale(1, 1, 2);
  rend_triangle(Py, 144);
  scale(1, 1, 0.5);//戻す
  
  //マフラー
  setcolor(1, 0, 0);
  translate(0, 5, -5.6);
  scale(4, 4, 4);
  rotateX(PI/2); 
  rend_triangle(To, 400);
  
  //帽子淵
  setcolor(0.3, 0.3, 0.3);
  translate(0, 0, 2.7);
  scale(0.7,0.7,0.7);
  rend_triangle(To, 400);
  
  //腕
  setcolor(0.6, 0.2, 0);
  translate(2, 0, -5);
  rotateX(3*PI/2); 
  rotateY(PI/2);
  rotateZ(3*PI/2);
  rotateY(PI/4);
  scale(0.25, 0.25, 4);
  rend_triangle(Py, 144);
  scale(4, 4, 0.25);//戻す 
  rotateY(-PI/4);
  rotateZ(-3*PI/2);
  rotateX(-3*PI/2); 

  translate(0, 2, 2);
  rotateX(PI/4);
  rotateZ(3*PI/2);
  scale(0.1, 0.1, 1);
  rend_triangle(Cy, 480);
  scale(10, 10, 1);//戻す
  rotateZ(-3*PI/2);
  rotateX(-PI/4);
  
  translate(0, -6, -2);
  rotateX(PI/4); 
  rotateZ(3*PI/2);
  scale(0.25, 0.25, 4);
  rend_triangle(Py, 144);
  scale(4, 4, 0.25);//戻す 
  
  
  translate(0, 0, 2);
  rotateX(PI/4);
  rotateZ(PI/4);
  rotateX(PI/3);
  rotateY(5*PI/4);
  scale(0.1, 0.1, 1);
  rend_triangle(Cy, 480);
  scale(10, 10, 1);//戻す
  rotateY(-5*PI/4);
  rotateX(-PI/3);
  rotateZ(-PI/4);
  rotateX(-PI/4);
  rotateZ(-3*PI/2);
  rotateX(-PI/4); 
  
  
  //帽子
  setcolor(0, 0, 1);
  translate(0, 3.4, 3.7);
  rotateZ(-PI/2);
  rend_triangle(Cy, 480);
  
  setcolor(1, 0, 0);
  translate(0, -2, -2.8);
  scale(0.3, 0.3, 0.3);
  rotateX(PI/2); 
  rend_triangle(To, 400);
  
  
 
  
  
  
  
 // rotateX(PI/2);
 //translate(-15, 0, 0);
  //rotateY(PI/);
  //rotateX(-PI/2);
 // scale(1, 0.6, 9);
  //rend_triangle(Py, 144);
  
 
  
  

//  scale(0.5, 0.5, 0.5);
//  setcolor(1, 0, 0);
//  rend_triangle(Py, 264);
//  setcolor(0, 1, 0);
//  translate(2, 0, 0);
//  rend_triangle(Py, 264);
//  setcolor(0, 0, 1);
//  translate(-4, 0, 0);
//  rend_triangle(Py, 264);
}

void draw_triangle(float [][][] Tr, int num) {
  //  (演習２－２) 
  for (int n=0; n<=num-1; n++) {
    beginShape(TRIANGLES);
    vertex(Tr[n][0][0], Tr[n][0][1], Tr[n][0][2]);
    vertex(Tr[n][1][0], Tr[n][1][1], Tr[n][1][2]);
    vertex(Tr[n][2][0], Tr[n][2][1], Tr[n][2][2]);
    endShape();
  }
}

void rend_triangle(float[][][] Tr, int num) {
  int n;
  float[] NV; 
  float br;
  NV = new float[3];
  for (n=0; n<num; n++) {






    Normal(Tr[n], NV);
    br=(NV[0]*Light[0]+NV[1]*Light[1]+NV[2]*Light[2])*255;
    fill(col[0]*br, col[1]*br, col[2]*br);
    beginShape(TRIANGLES);
    vertex();
    vertex(Tr[n][0][0], Tr[n][0][1], Tr[n][0][2]);
    vertex(Tr[n][1][0], Tr[n][1][1], Tr[n][1][2]);
    vertex(Tr[n][2][0], Tr[n][2][1], Tr[n][2][2]);
    endShape();
  }
}

void Normal(float[][] Tr, float[] N) {
  float r;
  float[] D1, D2;
  D1 = new float[3]; 
  D2 = new float[3];
  D1[0]=Tr[1][0]-Tr[0][0];
  D1[1]=Tr[1][1]-Tr[0][1];
  D1[2]=Tr[1][2]-Tr[0][2];

  D2[0]=Tr[2][0]-Tr[1][0];
  D2[1]=Tr[2][1]-Tr[1][1];
  D2[2]=Tr[2][2]-Tr[1][2];

  N[0]=D1[1]*D2[2]-D1[2]*D2[1]; 
  N[1]=D1[2]*D2[0]-D1[0]*D2[2];
  N[2]=D1[0]*D2[1]-D1[1]*D2[0];
  r=sqrt(N[0]*N[0]+N[1]*N[1]+N[2]*N[2]);
  N[0]/=r;
  N[1]/=r; 
  N[2]/=r;
}

void setcolor(float r, float g, float b) {
  col[0]=r; 
  col[1]=g; 
  col[2]=b;
}

void cal_c_point(float R, float t, float p, float[] pn) {
  float td=t*3.141592/180; 
  float pd=p*3.141592/180;
  pn[0]=(R*cos(pd))*cos(td);
  pn[1]=(R*cos(pd))*sin(td);
  pn[2]=R*sin(pd);
}

void TrmSphere(float[][][] tr, int num, float R)
{
  float d=360/num;  //横の分割
  int p=-(360/num)*5;//縦分割の初めの位置
  int t=0;
  int n=0;//三角形の数（四角形の生成用）
  int m=num*(num-2);//三角形の数（三角形の生成用）
  int b=num -10;

  for (int i=0;i<num-b;i++) {
    for (int j=0;j<num;j++)
    {
      //四角形の生成
      cal_c_point(R, t, p, tr[n][0]);
      cal_c_point(R, t+d, p+d, tr[n][1]);
      cal_c_point(R, t, p+d, tr[n][2]); 

      cal_c_point(R, t, p, tr[n+num*(num-b)][0]);
      cal_c_point(R, t+d, p, tr[n+num*(num-b)][1]);
      cal_c_point(R, t+d, p+d, tr[n+num*(num-b)][2]);

      //球の上・下部の三角形の生成
      cal_c_point(R, t, 180/2+d, tr[m][0]);
      cal_c_point(R, t+d, 180/2+d, tr[m][1]);
      cal_c_point(R, t, 180/2, tr[m][2]); 

      cal_c_point(R, t, -180/2+d, tr[m+num][0]);
      cal_c_point(R, t, -180/2, tr[m+num][1]);
      cal_c_point(R, t+d, -180/2+d, tr[m+num][2]);

      m++;
      n++;

      t +=d;
    }
    p+=d;
  }

  println(p);
  println(t);
}


// 以下ｈ、４．４で説明
void TrmPyramid(float[][][] tr, int num, float h, float r)
{
  float ddt=2.0*3.1415/num, dt;
  int i;
  for (i=0;i<num;i++) {
    dt=i*ddt;
    tr[i][0][0]=r*sin(dt);   
    tr[i][0][1]=r*cos(dt);   
    tr[i][0][2]=0;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=0;
    tr[i][2][0]=0;   
    tr[i][2][1]=0;   
    tr[i][2][2]=h;
  };
  for (int j=0;j<num;j++) {
    i=j+num;
    dt=j*ddt;
    tr[i][0][0]=r*sin(dt);   
    tr[i][0][1]=r*cos(dt);   
    tr[i][0][2]=0;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=0;
    tr[i][2][0]=0;   
    tr[i][2][1]=0;   
    tr[i][2][2]=0;
  };
}


void TrmCylinder(float[][][] tr, int num, float h, float r) { 

  //  この部分を完成させなさい（演習２－３）
  float ddt=2.0*3.1415/num, dt;
  int i, s, j, t;
  for (i=0;i<num;i++) {
    dt=i*ddt;
    tr[i][0][0]=r*sin(dt);   
    tr[i][0][1]=r*cos(dt);   
    tr[i][0][2]=0;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=0;
    tr[i][2][0]=r*sin(dt+ddt);   
    tr[i][2][1]=r*cos(dt+ddt);   
    tr[i][2][2]=h;
  };
  for (s=0;s<num;s++) {
    i= s+num;
    dt=s*ddt;
    tr[i][0][0]=r*sin(dt);   
    tr[i][0][1]=r*cos(dt);   
    tr[i][0][2]=0;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=h;
    tr[i][2][0]=r*sin(dt);   
    tr[i][2][1]=r*cos(dt);   
    tr[i][2][2]=h;
  };
  for (j=0;j<num;j++) {
    i =j+num*2;
    dt=j*ddt;
    tr[i][0][0]=r*sin(dt);   
    tr[i][0][1]=r*cos(dt);   
    tr[i][0][2]=0;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=0;
    tr[i][2][0]=0;   
    tr[i][2][1]=0;   
    tr[i][2][2]=0;
  };
  for (t=0;t<num;t++) {
    i =t+num*3;
    dt=t*ddt;
    tr[i][0][0]=r*sin(dt); 
    tr[i][0][1]=r*cos(dt); 
    tr[i][0][2]=h;
    tr[i][1][0]=r*sin(dt+ddt);
    tr[i][1][1]=r*cos(dt+ddt);
    tr[i][1][2]=h;
    tr[i][2][0]=0;   
    tr[i][2][1]=0;   
    tr[i][2][2]=h;
  };
}

void cal_t_point(float R, float sr, float t, float p, float[] pn) {
  float td=t*3.141592/180; 
  float pd=p*3.141592/180;
  pn[0]=(R+sr*cos(pd))*cos(td);
  pn[1]=(R+sr*cos(pd))*sin(td);
  pn[2]=sr*sin(pd);
}

void TrmTorus(float[][][] tr, int num1, int num2, float R, float r)
{
  //float ddt=2.0*3.1415/num, dt;
  float dt=360/num1;
  float dp=360/num2;
  int t=0;
  int p=0;
  int n=0;

  for (int i=0;i<num1;i++) {
    for (int j=0;j<num2;j++)
    {
      cal_t_point(R, r, t, p, tr[n][0]);
      cal_t_point(R, r, t+dt, p, tr[n][1]);
      cal_t_point(R, r, t, p+dp, tr[n][2]); 
      
      cal_t_point(R, r, t, p+dp, tr[n+200][0]);
      cal_t_point(R, r, t+dt, p, tr[n+200][1]);
      cal_t_point(R, r, t+dt, p+dp, tr[n+200][2]);
      n++;
      p +=dp;
     
    }
     t +=dt;
  }

 
}


  // キー操作，立体回転(矢印)とフレーム画像格納(s)
  void keyPressed() {
    if (keyCode == UP) { 
      rx = rx + 0.1;
    } // rotation angle
    else if (keyCode == DOWN) { 
      rx = rx - 0.1;
    }
    else if (keyCode == LEFT) { 
      rz = rz - 0.1;
    }
    else if (keyCode == RIGHT) { 
      rz = rz + 0.1;
    }
    else if ( key== 's' ) { 
      saveFrame(file_name);
    }  // Save File Name
  }

