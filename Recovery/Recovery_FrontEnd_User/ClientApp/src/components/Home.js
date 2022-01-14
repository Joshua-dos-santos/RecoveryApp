import React, { Component } from 'react';
import { Link, Redirect } from "react-router-dom";
import Slider from 'react-rangeslider';
import axios from 'axios'

export class Injury extends Component {
    static displayName = Injury.name;
    constructor(props) {
        super(props);

        this.state = {
            description: "",
            part_of_Body: "",
            pain_Scale: 0,
            userData: [],
            jtoken: sessionStorage.getItem("token"),
            loggedIn: false,
            loading: false
        };
        this.OnLoad = this.OnLoad.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    componentDidMount() {
        this.OnLoad();
    }

    OnLoad(e) {
        var self = this;
        console.log(sessionStorage.getItem("token"))
        axios({
            method: 'GET',
            url: 'http://localhost:5000/Account/GetUserByToken/GetUserByToken',
            params: {
                jtoken: sessionStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data }, () => { console.log(self.state.userData) });
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        var injury = {
            part_of_Body: this.state.part_of_Body,
            pain_Scale: this.state.pain_Scale,
            description: this.state.description,
        };
        const injurydata = injury;
        const userID = this.state.userData.unique_ID
        axios({
            method: 'POST',
            url: 'http://localhost:5000/api/injury/UpdateInjury',
            dataType: "json",
            data: { injurydata, userID }
        }).then(data => console.log(data));
    }

    handleScale = (value) => {
        var self = this;
        self.setState({
            pain_Scale: value
        })
    }

    handleChange(event) {
        let target = event.target;
        let value = target.type === "checkbox" ? target.checked : target.value;
        let name = target.name;

        this.setState({
            [name]: value
        });
    }

    render() {
        if (!sessionStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        let { pain_Scale } = this.state
        return (
            <div className="formCenter">
                <form onSubmit={this.handleSubmit} className="formFields">
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="part_of_Body">
                            Part of body
                        </label>
                        <input type="text" id="part_of_Body" className="formFieldInput" placeholder="Enter your first name" name="part_of_Body" value={this.state.part_of_Body} onChange={this.handleChange} />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="description">
                            Last Name
                        </label>
                        <input type="text" id="description" className="formFieldInput" placeholder="Enter a description" name="description" value={this.state.description} onChange={this.handleChange} />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="pain_Scale">
                            Pain Scale
                        </label>
                        <Slider min={1} max={10} value={pain_Scale} orientation="horizontal" onChange={this.handleScale}
                        />
                    </div>
                    <div className="formField">
                        <label className="formFieldCheckboxLabel">
                            <input
                                className="formFieldCheckbox"
                                type="checkbox"
                                name="hasAgreed"
                                value={this.state.hasAgreed}
                                onChange={this.handleChange}
                            />{" "}
                            I agree all statements in{" "}
                            <a href="null" className="formFieldTermsLink">
                                terms of service
                            </a>
                        </label>
                    </div>

                    <div className="formField">
                        <button className="formFieldButton" onClick={(e) => this.handleSubmit(e)} >Sign Up</button>{" "}
                    </div>
                </form>
            </div>
        );
    }
}
