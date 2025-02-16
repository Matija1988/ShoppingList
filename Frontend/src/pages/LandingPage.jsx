import { useUser } from "../context/UserContext"

export default function LandingPage() {

    const {username} = useUser();

    return(<>
    <div>
        <h1>Welcome {username}</h1>
    </div>
    </>)
}