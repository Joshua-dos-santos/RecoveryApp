import React, { Component, useEffect } from 'react';
import { Link } from 'react-router-dom';
import "./Login.css";
import { Login } from "./Login.js";
import axios from 'axios'
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';


export default class SignInForm extends Component {
    static displayName = SignInForm.name;
    constructor(props) {
        super(props)
        this.state = {
            email: '',
            password: '',
            token: '',
            hasError: false,
            errorMessage: '',
            loggedIn: false,
            token: null
        }
        this.handleLogin = this.handleLogin.bind(this)
    }

    componentDidUpdate() {
    }

    setSession = (token) => {
        ReactSession.set("loggedin", true);
        ReactSession.set("token", token.data);
        localStorage.setItem("Key", token);
        this.setState({ token: token.data, loggedIn: true });
    }

    handleLogin = (event) => {
        event.preventDefault();
        const email = this.state.mail
        const password = this.state.password

        this.setState({ hasError: false, errorMessage: '' })

        if (email == '' || email == null) {
            this.setState({ hasError: true, errorMessage: "Email must be filled in!" })
            return;
        } else if (password == '' || password == null) {
            this.setState({ hasError: true, errorMessage: "Password must be filled in!" })
            return;
        }

        var self = this;
        axios({
            method: 'post',
            url: 'https://localhost:44307/Account/Login/Login',
            data: { email, password }
        }).then(token => this.setSession(token)).catch(function (error) {
            if (error.message == "Request failed with status code 401") {
                self.setState({ hasError: true, errorMessage: "Email or Password is incorrect" })
            }

            return Promise.reject(error)
        });
    }
    render() {
        if (ReactSession.get("loggedin")) {
            return (
                <Redirect to="/diets" />
            )

        }

        return (
            <div className="formCenter">
                <form className="formFields" onSubmit={this.handleSubmit}>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="email">
                            E-Mail Address
                        </label>
                        <input
                            type="email"
                            id="email"
                            className="formFieldInput"
                            placeholder="Enter your email"
                            name="email"
                            value={this.state.email}
                            onChange={this.handleChange}
                        />
                    </div>

                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="password">
                            Password
                        </label>
                        <input
                            type="password"
                            id="password"
                            className="formFieldInput"
                            placeholder="Enter your password"
                            name="password"
                            value={this.state.password}
                            onChange={this.handleChange}
                        />
                    </div>

                    <div className="formField">
                        <button className="formFieldButton" onClick={(e) => this.handleLogin(e)}>Sign In</button>{" "}
                        <Link className="m-2 registerlink" to="/register">No account yet? Register here!</Link>
                    </div>
                </form>
            </div>
        );
    }

}