using System;
using System.Collections.ObjectModel;
using LogicLayer;

namespace ModelLayer
{
    public class Model
    {
        private readonly AbstractLogicAPI logicAPI;
        public ObservableCollection<BallAdapter> ObservableBallCollection { get; }

        public Model(AbstractLogicAPI logicAPI = null)
        {
            this.logicAPI = (logicAPI == null) ? AbstractLogicAPI.createLogicAPI() : logicAPI;
            this.logicAPI.createBalls(10);
            ObservableBallCollection = new ObservableCollection<BallAdapter>();
            foreach (Ball b in this.logicAPI.GetBalls())
            {
                ObservableBallCollection.Add(new BallAdapter(b));
            }
        }

        public void start()
        {
            logicAPI.start();
        }

        public void stop()
        {
            logicAPI.stop();
        }

        public void addBall()
        {
            logicAPI.createBall();
        }

        public void removeBall()
        {
            logicAPI.deleteBall();
        }
    }

}
