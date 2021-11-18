import React, { Component } from "react";
import { Link } from "react-router-dom";
import Slider from 'react-rangeslider';





export default class SignUpForm extends Component {
    constructor() {
        super();
        
        this.state = {
            email: "",
            password: "",
            birthday: null,
            first_name: "",
            last_name: "",
            height: 0,
            weight: 0,
            physical_therapist: "",
            hasAgreed: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleHeight = (value) => {
        this.setState({
            height: value
        })
    }
    handleWeight = (value) => {
        this.setState({
            weight: value
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

    handleSubmit(e) {
        e.preventDefault();

        console.log("The form was submitted with the following data:");
        console.log(this.state);
    }



    render() {
        let { height } = this.state
        let {weight} = this.state
        return (
            <div className="formCenter">
                <form onSubmit={this.handleSubmit} className="formFields">
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="first_name">
                            First Name
                        </label>
                        <input type="text" id="first_name" className="formFieldInput" placeholder="Enter your first name" name="first_name" value={this.state.first_name} onChange={this.handleChange} />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="last_name">
                            Last Name
                        </label>
                        <input type="text" id="last_name" className="formFieldInput" placeholder="Enter your last name" name="last_name" value={this.state.last_name} onChange={this.handleChange} />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="first_name">
                            Birthday
                        </label>

                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="height">
                            Height
                        </label>
                        <Slider min={150} max={210} value={height} orientation="horizontal" onChange={this.handleHeight}
                        />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="weight">
                            Weight
                        </label>
                        <Slider min={30} max={150} value={weight} orientation="horizontal" onChange={this.handleWeight}
                        />
                    </div>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="password">
                            Password
                        </label>
                        <input type="password" id="password" className="formFieldInput" placeholder="Enter your password" name="password" value={this.state.password} onChange={this.handleChange} />
                    </div>
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
                        <label className="formFieldLabel" htmlFor="physical_therapist">
                            Physical Therapist Key
                        </label>
                        <input type="text" id="physical_therapist" className="formFieldInput" placeholder="Enter your PT-Key" name="physical_therapist" value={this.state.physical_therapist} onChange={this.handleChange} />
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
                        <button className="formFieldButton">Sign Up</button>{" "}
                        <Link to="/login" className="formFieldLink">
                            I'm already member
                        </Link>
                    </div>
                </form>
            </div>
        );
    }
}

