import { useState } from 'react'
import './App.css'
import { Route, Routes } from 'react-router-dom'
import { RouteNames } from './constants/constants'
import LogIn from './pages/Login'
import LandingPage from './pages/LandingPage'
import "bootstrap/dist/css/bootstrap.min.css";
import Products from './pages/product/Product'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Routes>
        <Route path={RouteNames.HOME} element={<LogIn/>}></Route>
        <Route path={RouteNames.LANDINGPAGE} element={<LandingPage/>}></Route>
        <Route path={RouteNames.PRODUCTS} element={<Products/>}></Route>
      </Routes>
    </>
  )
}

export default App
