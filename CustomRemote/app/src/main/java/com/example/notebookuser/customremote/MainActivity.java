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

public class MainActivity extends AppCompatActivity {
    public final static String EXTRA_CATEGORY = "com.example.notebookuser.customremote.Ca";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        new Statics().init();
        refresh();
    }

    @Override
    protected void onResume()
    {
        super.onResume();
        refresh();
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

    public void refresh()
    {
        try {
            String category = this.getIntent().getStringExtra(MainActivity.EXTRA_CATEGORY);
            LinearLayout main_layout = (LinearLayout)findViewById(R.id.main_layout);
            main_layout.removeAllViews();
            for (final ActionPair actionPair : new TcpHelper().getAllCommands(category)) {
                if (actionPair.getCommand()==getString(R.string.error))
                    throw new Exception(actionPair.getName());

                Button b = new Button(this);
                b.setTag(actionPair);
                b.setText(!actionPair.isEnabled() ? getString(R.string.executing) : actionPair.getName());
                b.setEnabled(actionPair.isEnabled());
                actionPair.setButton(b);

                if (actionPair.isCategory())
                    b.getBackground().setColorFilter(new LightingColorFilter(Color.WHITE, Color.DKGRAY));

                final MainActivity _this = this;

                b.setOnClickListener(new View.OnClickListener() {
                    public void onClick(View v) {
                        final Button b = (Button) v;
                        if (!actionPair.isCategory()) {
                            b.setText(R.string.executing);
                            b.setEnabled(false);
                            new Thread(new Runnable() {
                                public void run() {
                                    new TcpHelper().Do(
                                            ((ActionPair) b.getTag()).getCommand(),
                                            ((ActionPair) b.getTag()).getName()
                                    );
                                }
                            }).start();
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
            Intent intent = new Intent(this, ChangeAddressActivity.class);
            startActivity(intent);

            Dialog d = new Dialog(this);
            d.setTitle(e.getMessage());
            d.show();
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
