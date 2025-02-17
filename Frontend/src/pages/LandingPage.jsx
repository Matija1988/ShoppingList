import NavBar from "../components/NavBar";
import useAuthStore from "../storage/authStore";

export default function LandingPage() {

    const {user} = useAuthStore();

    return(<>
<NavBar />
    <div>
        <h1>W</h1>
        <h1>Welcome {user}</h1>
    </div>
    </>)
}