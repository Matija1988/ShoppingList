import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Route, Routes } from 'react-router-dom'
import { RouteNames } from './constants/constants'
import LogIn from './pages/Login'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Routes>
        <Route path={RouteNames.HOME} element={<LogIn/>}></Route>

      </Routes>
    </>
  )
}

export default App
