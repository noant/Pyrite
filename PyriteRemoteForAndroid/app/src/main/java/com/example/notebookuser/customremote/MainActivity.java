package com.example.notebookuser.customremote;

import android.app.Dialog;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.LightingColorFilter;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

public class MainActivity extends AppCompatActivity {
    public final static String EXTRA_CATEGORY = "com.example.notebookuser.customremote.Ca";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    protected void onStart(){
        super.onStart();
        new Statics().init();
        refresh();
        if (!_timerStarted)
            beginTimer();
    }

    @Override
    protected void onResume()
    {
        super.onResume();
        refresh(false);
    }

    private LinearLayout _main_layout;
    private LinearLayout getMainLayout(){
        if (_main_layout != null)
            return _main_layout;

        LinearLayout main_layout = (LinearLayout)findViewById(R.id.main_layout);
        if (main_layout == null) {
            setContentView(R.layout.activity_main);
            main_layout = (LinearLayout)findViewById(R.id.main_layout);
        }
        return main_layout;
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event)
    {
        if (keyCode==KeyEvent.KEYCODE_MENU)
        {
            Intent intent = new Intent(this, ChangeAddressActivity.class);
            startActivity(intent);
        }
        return super.onKeyDown(keyCode, event);
    }

    public void btClick(View v)
    {
        Intent intent = new Intent(this, ChangeAddressActivity.class);
        startActivity(intent);
    }
    boolean _timerStarted;
    public void beginTimer()
    {
        UpdateHandler.Tcp.AllActivities.add(this);

        if (!UpdateHandler.Tcp.isStarted())
            UpdateHandler.Tcp.beginHandling();

        new Thread(new Runnable() {
            @Override
            public void run() {
                while (true) {
                    try {
                        Thread.sleep(1000 * 60);
                        getMainLayout().post(new Runnable() {
                            @Override
                            public void run() {
                                refresh(false);
                            }
                        });
                        }
                    catch (Exception e){}
                }
            }
        }).start();
        _timerStarted=true;
    }

    public  void refresh(){
        refresh(true);
    }

    private void refresh(boolean errorHandling)
    {
        try {
            String category = this.getIntent().getStringExtra(MainActivity.EXTRA_CATEGORY);
            LinearLayout main_layout = getMainLayout();
            main_layout.removeAllViews();
            for (final ActionPair actionPair : new TcpHelper().getAllCommands(category)) {
                final Button b = new Button(this);
                b.setTag(actionPair);

                if (actionPair.isCategory()) {
                    b.getBackground().setColorFilter(new LightingColorFilter(Color.WHITE, Color.DKGRAY));
                    b.setText(actionPair.getCommand());
                }
                else {
                    b.setText(actionPair.getStatus());
                    actionPair.setOnStatusUpdated(new ActionPairRunnable() {
                        @Override
                        public void run(final ActionPair actionPair) {
                            b.post(new Runnable() {
                                @Override
                                public void run() {
                                    if (b.getParent() == null)
                                        actionPair.needDelete();
                                    else {
                                        b.setText(actionPair.getStatus());
                                        b.setEnabled(true);
                                    }
                                }
                            });
                        }
                    });
                }

                final MainActivity _this = this;

                b.setOnClickListener(new View.OnClickListener() {
                    public void onClick(View v) {
                        if (!actionPair.isCategory()) {
                            b.setText(R.string.executing);
                            b.setEnabled(false);
                            actionPair.Do();
                        } else {
                            Intent intent = new Intent(_this, MainActivity.class);
                            intent.putExtra(EXTRA_CATEGORY, b.getText());
                            startActivity(intent);
                        }
                    }
                });
                LinearLayout.LayoutParams lParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, 120);
                main_layout.addView(b, lParams);
            }
        }
        catch (Exception e) {
            if (errorHandling) {
                Intent intent = new Intent(this, ChangeAddressActivity.class);
                startActivity(intent);

                Dialog d = new Dialog(this);
                d.setTitle(e.getMessage());
                d.show();
            }
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
