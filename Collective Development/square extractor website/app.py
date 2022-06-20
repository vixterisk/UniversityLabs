from flask import Flask, render_template

app = Flask(__name__)


@app.route('/')
def main_page():
    return render_template('index.html')

@app.route('/support')
def support_page():
    return render_template('support.html')

@app.route('/documentation')
def documentation_page():
    return render_template('documentation.html')