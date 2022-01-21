import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Injury } from './components/Home';
import { Diets } from './components/Diet';
import { UserInfo } from './components/UserInfo';
import './custom.css'
import SignInForm from './pages/SignInForm';
import SignUpForm from './pages/SignUpForm';
import { Exercises } from './components/Exercises';
import { UserDashboard } from './components/UserDashboard';


export default class App extends Component {
    static displayName = App.name;

    render() { 
        return (
            <Layout>
                <Route exact path='/' component={SignInForm} />
                <Route exact path='/injury' component={Injury} />
                <Route path='/diets' component={Diets} />
                <Route path="/login" component={SignInForm} />
                <Route path="/register" component={SignUpForm} />
                <Route path="/exercises" component={Exercises} />
                <Route path="/userInfo" component={UserInfo} />
                <Route path="/dashboard" component={UserDashboard} />
            </Layout>
        );
    }
}
